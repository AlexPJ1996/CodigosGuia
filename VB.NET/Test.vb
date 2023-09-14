    Dim MaxForm As Boolean
    Dim FAnc As String = Me.Width
    Dim FAlt As String = Me.Height
    Sub MaxResForm()
        If MaxForm <> True Then
            Me.Location = Screen.PrimaryScreen.WorkingArea.Location
            Me.Size = Screen.PrimaryScreen.WorkingArea.Size
            MaxForm = True
        Else
            Me.Width = FAnc
            Me.Height = FAlt
            Me.Location = New Point(100, 100)
            MaxForm = False
        End If
    End Sub
