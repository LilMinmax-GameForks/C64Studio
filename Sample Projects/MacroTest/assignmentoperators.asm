* = $2000

label1 = 5
label1 *= 4

!byte label1

!macro hurz
  !message "inside macro"
!end


!message "outside macro"

value = 2
;value += 3

temp = 2

value /= temp

gnu = "hurz"
gnu += "lsmf"

!message "value is ",value

+hurz