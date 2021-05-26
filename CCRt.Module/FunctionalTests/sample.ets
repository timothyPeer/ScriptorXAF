;#DropDB CCRtEasyTest

#Application CCRtWin
#Application CCRtWeb

*FillForm
 User Name = Admin
*Action Log In

#IfDef CCRtWin

*Action About...
 
*Action OK

#EndIf

#IfDef CCRtWeb
*Action Navigation(My Details)
#EndIf
