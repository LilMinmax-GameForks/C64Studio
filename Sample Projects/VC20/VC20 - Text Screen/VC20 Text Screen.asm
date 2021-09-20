!source <vc20.asm>

*=$1001

!basic

          ldx #0

-
          lda SCREEN_DATA,x
          sta $1e00,x
          lda SCREEN_DATA + 253,x
          sta $1e00 + 253,x

          lda SCREEN_DATA + 23 * 22,x
          sta $9600,x
          lda SCREEN_DATA + 23 * 22 + 253,x
          sta $9600 + 253,x

          inx
          cpx #253
          bne -

          lda #$53 | $08
          sta VIC.COLORS

          lda #$e0
          sta VIC.AUX_COLOR_VOLUME

          jmp *




SCREEN_DATA
          !media "VC20 - Text Screen.charscreen",CHARCOLOR