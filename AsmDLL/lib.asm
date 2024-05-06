bmp_dst	equ 0
bmp_start equ 16
bmp_end equ 20
maxValue equ 24

histogram_size equ (256 * 4)
stride equ (256 * 3)
bottom equ (138-5)

.data

histHeight  dd 138

.code

; rcx - adres tablicy z liczbą wystąpień pikseli
; rdx - struktura bmpDrawLines
CreateSubHistogram proc
	push rdi
	push rsi
	push rbx

	mov rsi, rcx
	mov r8d, dword ptr[rdx + bmp_end]
	lea r8, [rsi+r8*4]
	
	; RBX określa numer kolumny zaznaczanego piksela
	mov ebx, dword ptr[rdx + bmp_start]
	lea rsi, [rsi+rbx*4]

	mov rdi, qword ptr[rdx + bmp_dst]
	
	; alokacja 48 bajtów na stosie na zapisanie wyników obliczeń:
	sub rsp, 16*3
	mov r10, rsp

	; Inicjalizacja XMM15
	vbroadcastss xmm14, dword ptr[rdx+maxValue]
	vbroadcastss xmm15, dword ptr[histHeight]
	cvtdq2ps xmm14, xmm14
	cvtdq2ps xmm15, xmm15 
	divps xmm15, xmm14
	
	; XMM15 = 1/varMAX * histHeight [float]
	; Powyższa stała posłuży do przeliczania wartości poszczególnych pikseli
	
	; poniższe 3 instrukcje to: RBX *= 3   (faktycznie: RBX = RBX * 2 + RBX)
	mov rax, rbx
	shl rbx, 1
	add rbx, rax

	; Aktualny stan rejestrów:
	;---------------------------------
	; RSI: adres tablicy z histogramem
	; RDI: adres danych, do których należy zapisywać piksele obrazu histogramu
	; RBX: numer pierwszego bajta w wierszu do zapisania koloru (FF)
	; R8: adres graniczny RSI (terminator pętli)
petla_histogram:
	; Do rejestrów XMM0, 1 i 2 ładujemy odpowiednio: 4 kolejne wartości dla R, G i B
	movdqu xmm0, [rsi]
	movdqu xmm1, [rsi+histogram_size]
	movdqu xmm2, [rsi+histogram_size*2]
	
	; Do rejestrów XMM3,4,5 kopiujemy odpowiednio XMM0,1,2, konwertując je przy tym na float
	cvtdq2ps xmm3, xmm0
	cvtdq2ps xmm4, xmm1
	cvtdq2ps xmm5, xmm2
	
	; Mnożymy każdą ze składowych przez wartość: (histHeight / maxValue)
	mulps xmm3, xmm15
	mulps xmm4, xmm15
	mulps xmm5, xmm15
	
	; XMM3,4,5 zawierają teraz proporcjonalnie przemnożone składowe, wskazujące na najwyższy piksel obrazu do wykreślenia odcinka
	cvtps2dq xmm3, xmm3
	cvtps2dq xmm4, xmm4
	cvtps2dq xmm5, xmm5
	
	; Zapis tymczasowych wyników do pamięci, na stosie
	movdqu [r10], xmm3
	movdqu [r10+16], xmm4
	movdqu [r10+32], xmm5
	
	; Inicjalizacja RCX: będzie wskazywał na kolejny piksel (kolumnę) z przetwarzanej aktualnie części obrazu
	xor rcx, rcx
odcinki:
	; Załaduj do EDX bieżącą wartość składowej B; jeśli > 0, odpal procedurę do wykreślenia odcinka
	mov edx, dword ptr[r10+rcx + 32]
	test edx, edx
	jz _next1
	call DrawLine
	_next1:
	inc rbx
	
	; Analogicznie jak powyżej - składowa zielona
	mov edx, dword ptr[r10+rcx + 16]
	test edx, edx
	jz _next2
	call DrawLine
	_next2:
	inc rbx
	
	; Analogicznie jak powyżej - składowa czerwona
	mov edx, dword ptr[r10+rcx]
	test edx, edx
	jz _next3
	call DrawLine
	_next3:
	inc rbx
	
	; Przesuwamy ECX o 4 bajty dalej - przetwarzamy kolejny piksel wstępnie obrobiony w XMM
	add ecx, 4
	cmp ecx, 16
jb odcinki
	
	; Inkrementacja RSI o 16 - tyle danych jednocześnie wczytujemy do rejestrów XMM0-2
	add rsi, 16
	cmp rsi, r8
jb petla_histogram

	; Zwolnienie tymczasowej pamięci na stosie
	add rsp, 16*3
	
	; Przywrócenie rejestrów, wyjście z procedury
	pop rbx
	pop rsi
	pop rdi
	ret
CreateSubHistogram endp



; Parametry:
; EDI: Adres tablicy do zapisu pikseli
; EDX: Długość odcinka licząc od dołu (bottom) do góry
; RBX: przesunięcie punktu względem początku linii
DrawLine:
	; R9 inicjalizowany na adres piksela w pamięci obrazu
	; Zaczynamy od dołu, iterujemy w pętli do góry
	lea r9, [rbx + bottom * stride]
	
next_point:
	; Zapis wartości składowej FF (maksymalna) do bieżącego bajtu piksela
	; RBX wskazuje na konkretny bajt: każde wywołanie tej procedury wypełnia tylko jedną składową.
	; W jednym wywołaniu narysuje czerwony pasek, w drugim zielony, w trzecim niebieski.
	; Każdy z w/w pasków może mieć inną długość, dlatego nie robimy tego "za jednym zamachem".
	; Wszystkie paski ostatecznie łączą się w jeden, wielokolorowy.
	mov byte ptr[rdi+r9], 0FFh
	
	; Odjęcie wielkości stride od R9 - przesuwamy się o jeden wiersz do góry
	sub r9, stride
	
	; RDX pilnuje pętli - w momencie wyzerowania następuje powrót z pod-procedury.
	dec rdx
jnz next_point

ret

end
