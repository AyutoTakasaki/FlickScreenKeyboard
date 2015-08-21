Imports System.Runtime.InteropServices
Imports System.Drawing
Imports System.Drawing.Drawing2D

Public Class Form1

    <DllImport("user32.dll")> Private Shared Function GetForegroundWindow() As IntPtr
    End Function
    <DllImport("user32.dll")> Private Shared Function SetForegroundWindow(ByVal hWnd As IntPtr) As Integer
    End Function
    <DllImport("user32.dll")> Private Shared Function GetWindowThreadProcessId(ByVal hwnd As IntPtr, ByRef lpdwProcessId As Integer) As Integer
    End Function
    <DllImport("user32.dll")> Private Shared Function AttachThreadInput(ByVal idAttach As Integer, ByVal idAttachTo As Integer, ByVal fAttach As Boolean) As Boolean
    End Function

    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        Const WM_MOUSEACTIVATE = &H21
        Const MA_NOACTIVATE = 3
        '        Const MA_NOACTIVATEANDEAT = 4
        If m.Msg = WM_MOUSEACTIVATE Then
            m.Result = New IntPtr(MA_NOACTIVATE)
            Return
        End If
        MyBase.WndProc(m)
    End Sub


    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick

        Static ThreadProcessId As Integer = GetWindowThreadProcessId(Me.Handle, 0&)
        Static OldWindowHandle As IntPtr = IntPtr.Zero
        Dim targetWindowHandle As IntPtr = GetForegroundWindow()

        If targetWindowHandle = Me.Handle AndAlso OldWindowHandle <> IntPtr.Zero Then
            Me.TopMost = True
            SetForegroundWindow(OldWindowHandle)
        Else
            If targetWindowHandle <> OldWindowHandle Then
                Dim targetThreadProcessId As Integer = GetWindowThreadProcessId(targetWindowHandle, 0&)
                AttachThreadInput(ThreadProcessId, targetThreadProcessId, True)
                OldWindowHandle = targetWindowHandle
            End If
        End If

    End Sub

    Protected Overrides Sub OnPaintBackground(ByVal pevent As System.Windows.Forms.PaintEventArgs)

    End Sub


    Dim MainButtons(20) As Label
    Dim SubButtons(9) As Label

    'mode 0 = 平仮名
    'mode 1 = カタカナ
    'mode 2 = アルファベット

    Dim mode As Integer = 0

    'mode2 0 = 通常
    'mode2 1 = 小文字など

    Dim mode2 As Integer = 0

    Dim FlickButtonIndexes() As Integer = {4, 5, 6, 8, 9, 10, 11, 12, 13, 14, 15}
    Dim NotFlickButtonIndexes() As Integer = {0, 1, 2, 3, 7, 16, 17, 18, 19}

    Dim Hiragana As Hashtable = New Hashtable
    Dim Katakana As Hashtable = New Hashtable
    Dim Alphabet As Hashtable = New Hashtable
    Dim HiraganaDakuHalfSmall As Hashtable = New Hashtable
    Dim KatakanaDakuHalfSmall As Hashtable = New Hashtable
    Dim AlphabetSmall As Hashtable = New Hashtable

    Dim IsMouseDown As Boolean = False
    Dim FlickingButton As Integer = Nothing
    Dim NearFlickingButton(4) As Integer
    Dim LastSelectedButton As Integer

    Dim NewButtonText() As Char

    Private Sub Form1_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        PutButtons()
        Timer1.Start()
        Me.SetStyle(ControlStyles.Opaque, True)

        Hiragana.Add(4, "あいうえお")
        Katakana.Add(4, "アイウエオ")

        HiraganaDakuHalfSmall.Add(4, "ぁぃぅぇぉ")
        KatakanaDakuHalfSmall.Add(4, "ァィゥェォ")

        Hiragana.Add(8, "かきくけこ")
        Katakana.Add(8, "カキクケコ")

        HiraganaDakuHalfSmall.Add(8, "がぎぐげご")
        KatakanaDakuHalfSmall.Add(8, "ガギグゲゴ")

        Hiragana.Add(12, "さしすせそ")
        Katakana.Add(12, "サシスセソ")

        HiraganaDakuHalfSmall.Add(12, "ざじずぜぞ")
        KatakanaDakuHalfSmall.Add(12, "ザジズゼゾ")

        Hiragana.Add(5, "たちつてと")
        Katakana.Add(5, "タチツテト")

        HiraganaDakuHalfSmall.Add(5, "だぢづでど")
        KatakanaDakuHalfSmall.Add(5, "ダヂヅデド")

        Hiragana.Add(9, "なにぬねの")
        Katakana.Add(9, "ナニヌネノ")

        HiraganaDakuHalfSmall.Add(9, "ぱぴぷぺぽ")
        KatakanaDakuHalfSmall.Add(9, "パピプペポ")

        Hiragana.Add(13, "はひふへほ")
        Katakana.Add(13, "ハヒフヘホ")

        HiraganaDakuHalfSmall.Add(13, "ばびぶべぼ")
        KatakanaDakuHalfSmall.Add(13, "バビブベボ")

        Hiragana.Add(6, "まみむめも")
        Katakana.Add(6, "マミムメモ")

        HiraganaDakuHalfSmall.Add(6, "　　っ　　")
        KatakanaDakuHalfSmall.Add(6, "　　ッ　　")

        Hiragana.Add(10, "や ゆ　よ")
        Katakana.Add(10, "ヤ ユ　ヨ")

        HiraganaDakuHalfSmall.Add(10, "ゃ　ゅ　ょ")
        KatakanaDakuHalfSmall.Add(10, "ャ　ュ　ョ")

        Hiragana.Add(14, "らりるれろ")
        Katakana.Add(14, "ラリルレロ")

        Hiragana.Add(11, "わを　ん　")
        Katakana.Add(11, "ワヲ　ン　")

        HiraganaDakuHalfSmall.Add(14, "　　　　　")
        KatakanaDakuHalfSmall.Add(14, "　　　　　")

        HiraganaDakuHalfSmall.Add(11, "　　　　　")
        KatakanaDakuHalfSmall.Add(11, "　　　　　")

        Hiragana.Add(15, "、？　。　")
        Katakana.Add(15, "、？　。　")
        HiraganaDakuHalfSmall.Add(15, "、？　。　")
        KatakanaDakuHalfSmall.Add(15, "、？　。　")


        Alphabet.Add(4, ".@ 　1")
        Alphabet.Add(8, "ABC　2")
        Alphabet.Add(12, "DEF　3")

        Alphabet.Add(5, "GHI　4")
        Alphabet.Add(9, "JKL　5")
        Alphabet.Add(13, "MNO　6")

        Alphabet.Add(6, "PQRS　7")
        Alphabet.Add(10, "TUV　8")
        Alphabet.Add(14, "WXYZ9")

        Alphabet.Add(11, "　　　　0")
        Alphabet.Add(15, ",.　?　")



        AlphabetSmall.Add(4, ".@　　1")
        AlphabetSmall.Add(8, "abc　2")
        AlphabetSmall.Add(12, "def　3")

        AlphabetSmall.Add(5, "ghi　4")
        AlphabetSmall.Add(9, "jkl　5")
        AlphabetSmall.Add(13, "mno　6")

        AlphabetSmall.Add(6, "pqrs　7")
        AlphabetSmall.Add(10, "tuv　8")
        AlphabetSmall.Add(14, "wxyz9")

        AlphabetSmall.Add(11, "　　　　0")
        AlphabetSmall.Add(15, ",.　?　")

    End Sub

    Private Sub PutButtons()

        For i As Integer = 0 To 20
            MainButtons(i) = New Label
            MainButtons(i).Dock = DockStyle.Fill
            MainButtons(i).Margin = New Padding(1)
            MainButtons(i).TextAlign = ContentAlignment.MiddleCenter
            MainButtons(i).Font = New Font("メイリオ", 14)
            MainButtons(i).BorderStyle = BorderStyle.FixedSingle
            MainButtons(i).BackColor = Color.Black
            MainButtons(i).ForeColor = Color.White
        Next

        For i As Integer = 0 To 9
            SubButtons(i) = New Label
            SubButtons(i).Dock = DockStyle.Fill
            SubButtons(i).Margin = New Padding(1)
            SubButtons(i).TextAlign = ContentAlignment.MiddleCenter
            SubButtons(i).Font = New Font("メイリオ", 10)
        Next

        Dim ItemCount As Integer = 0
        For i As Integer = 0 To 4
            For ii As Integer = 0 To 3
                ButtonTable.Controls.Add(MainButtons(ItemCount), i, ii)
                ItemCount += 1
            Next
        Next

        ItemCount = 0
        For i As Integer = 0 To 2
            For ii As Integer = 0 To 2
                FlickNaviPanel.Controls.Add(SubButtons(ItemCount), i, ii)
                ItemCount += 1
            Next
        Next

        ChangeButtonText()


        For Each i As Integer In FlickButtonIndexes
            AddHandler MainButtons(i).MouseDown, AddressOf StartFlick
            AddHandler MainButtons(i).MouseMove, AddressOf Flicking
            AddHandler MainButtons(i).MouseUp, AddressOf EnterFlick
            MainButtons(i).BackColor = Color.White
            MainButtons(i).ForeColor = Color.Black
        Next

        For Each i As Integer In NotFlickButtonIndexes
            AddHandler MainButtons(i).MouseDown, AddressOf ClickButtonDown
            AddHandler MainButtons(i).MouseUp, AddressOf ClickButtonUp
        Next

    End Sub

    Private Sub ChangeButtonText()


        MainButtons(0).Text = ""
        MainButtons(20).Text = ""

        MainButtons(0).Text = "←"
        MainButtons(16).Text = "→"

        MainButtons(1).Text = "IME"
        MainButtons(2).Text = "片"
        MainButtons(3).Text = "記号"

        MainButtons(7).Font = New Font("メイリオ", 10)
        MainButtons(7).Text = "小／濁"

        MainButtons(17).Font = New Font("メイリオ", 10)
        MainButtons(17).Text = "BS"

        MainButtons(18).Font = New Font("メイリオ", 10)
        MainButtons(18).Text = "Space"

        MainButtons(19).Font = New Font("メイリオ", 10)
        MainButtons(19).Text = "Enter"

        Select Case mode
            Case 0
                If mode2 = 0 Then
                    MainButtons(4).Text = "あ"
                    MainButtons(8).Text = "か"
                    MainButtons(12).Text = "さ"
                    MainButtons(5).Text = "た"
                    MainButtons(9).Text = "な"
                    MainButtons(13).Text = "は"
                    MainButtons(6).Text = "ま"
                    MainButtons(10).Text = "や"
                    MainButtons(14).Text = "ら"
                    MainButtons(11).Text = "わ"
                    MainButtons(15).Text = "、"
                Else
                    MainButtons(4).Text = "ぁ"
                    MainButtons(8).Text = "が"
                    MainButtons(12).Text = "ざ"
                    MainButtons(5).Text = "だ"
                    MainButtons(9).Text = "ぱ"
                    MainButtons(13).Text = "ば"
                    MainButtons(6).Text = "っ"
                    MainButtons(10).Text = "ゃ"
                    MainButtons(14).Text = ""
                    MainButtons(11).Text = ""
                    MainButtons(15).Text = "、"
                End If
            Case 1
                If mode2 = 0 Then
                    MainButtons(4).Text = "ア"
                    MainButtons(8).Text = "カ"
                    MainButtons(12).Text = "サ"
                    MainButtons(5).Text = "タ"
                    MainButtons(9).Text = "ナ"
                    MainButtons(13).Text = "ハ"
                    MainButtons(6).Text = "マ"
                    MainButtons(10).Text = "ヤ"
                    MainButtons(14).Text = "ラ"
                    MainButtons(11).Text = "ワ"
                    MainButtons(15).Text = "、"
                Else
                    MainButtons(4).Text = "ァ"
                    MainButtons(8).Text = "ガ"
                    MainButtons(12).Text = "ザ"
                    MainButtons(5).Text = "ダ"
                    MainButtons(9).Text = "パ"
                    MainButtons(13).Text = "バ"
                    MainButtons(6).Text = "ッ"
                    MainButtons(10).Text = "ャ"
                    MainButtons(14).Text = ""
                    MainButtons(11).Text = ""
                    MainButtons(15).Text = "、"
                End If
            Case 2

                If mode2 = 0 Then
                    MainButtons(4).Text = "." & vbCrLf & "1"
                    MainButtons(8).Text = "ABC" & vbCrLf & "2"
                    MainButtons(12).Text = "DEF" & vbCrLf & "3"
                    MainButtons(5).Text = "GHI" & vbCrLf & "4"
                    MainButtons(9).Text = "JKL" & vbCrLf & "5"
                    MainButtons(13).Text = "MNO" & vbCrLf & "6"
                    MainButtons(6).Text = "PQRS" & vbCrLf & "7"
                    MainButtons(10).Text = "TUV" & vbCrLf & "8"
                    MainButtons(14).Text = "WXYZ" & vbCrLf & "9"
                    MainButtons(11).Text = "　" & vbCrLf & "0"
                    MainButtons(15).Text = ","
                Else
                    MainButtons(4).Text = "." & vbCrLf & "1"
                    MainButtons(8).Text = "abc" & vbCrLf & "2"
                    MainButtons(12).Text = "def" & vbCrLf & "3"
                    MainButtons(5).Text = "ghi" & vbCrLf & "4"
                    MainButtons(9).Text = "jkl" & vbCrLf & "5"
                    MainButtons(13).Text = "mno" & vbCrLf & "6"
                    MainButtons(6).Text = "pqrs" & vbCrLf & "7"
                    MainButtons(10).Text = "tuv" & vbCrLf & "8"
                    MainButtons(14).Text = "wxyz" & vbCrLf & "9"
                    MainButtons(11).Text = "　" & vbCrLf & "0"
                    MainButtons(15).Text = ","
                End If
        End Select

    End Sub

    Private Sub ClickButtonDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)

        Dim i As Integer = 0
        While (Not (sender Is MainButtons(i)))
            i += 1
        End While

        FlickingButton = i

        MainButtons(FlickingButton).ForeColor = Color.Black
        MainButtons(FlickingButton).BackColor = Color.White

        IsMouseDown = True
        SameKey.Start()

    End Sub

    Private Sub ClickButtonUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)

        MainButtons(FlickingButton).ForeColor = Color.White
        MainButtons(FlickingButton).BackColor = Color.Black

        SameKey.Stop()
        ClickButton()
        IsMouseDown = False

    End Sub

    Private Sub ClickButton()

        Select Case FlickingButton
            Case 2
                If mode = 0 Then
                    mode = 1
                    ChangeButtonText()
                    MainButtons(FlickingButton).Text = "英"
                ElseIf mode = 1 Then
                    For Each i As Integer In FlickButtonIndexes
                        MainButtons(i).Font = New Font("メイリオ", 10)
                    Next
                    mode = 2
                    ChangeButtonText()
                    MainButtons(FlickingButton).Text = "平"
                ElseIf mode = 2 Then
                    For Each i As Integer In FlickButtonIndexes
                        MainButtons(i).Font = New Font("メイリオ", 14)
                    Next
                    mode = 0
                    ChangeButtonText()
                    MainButtons(FlickingButton).Text = "片"
                End If
            Case 7
                If mode2 = 0 Then
                    mode2 = 1
                    ChangeButtonText()
                    MainButtons(FlickingButton).Text = "通常"
                ElseIf mode2 = 1 Then
                    mode2 = 0
                    ChangeButtonText()
                    MainButtons(FlickingButton).Text = "小／濁"
                End If
            Case 17
                SendKeys.SendWait("{BackSpace}")
            Case 18
                SendKeys.SendWait(" ")
            Case 19
                SendKeys.SendWait("{Enter}")
            Case 0
                SendKeys.SendWait("{Left}")
            Case 16
                SendKeys.SendWait("{Right}")
            Case 1

        End Select
    End Sub

    Private Sub SameKey_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SameKey.Tick
        If IsMouseDown = True Then
            ClickButton()
        Else
            Console.WriteLine(IsMouseDown)
            SameKey.Stop()
        End If
    End Sub


    Private Sub StartFlick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)

        Dim i As Integer = 0
        While (Not (sender Is MainButtons(i)))
            i += 1
        End While

        FlickingButton = i

        MainButtons(FlickingButton).ForeColor = Color.White
        MainButtons(FlickingButton).BackColor = Color.DeepSkyBlue

        IsMouseDown = 1

        If FlickingButton = 1 Then

        End If

        Dim NewX As Integer = 0
        Dim NewY As Integer = 0

        NewX = MainButtons(FlickingButton).Location.X

        NewX -= (Integer.Parse(FlickNaviPanelPanel.Width) - Integer.Parse(MainButtons(FlickingButton).Width)) / 2

        NewY = MainButtons(FlickingButton).Location.Y

        If System.Text.RegularExpressions.Regex.Match(FlickingButton.ToString, "^(5|6|9|10|13|14)$").Value <> "" Then
            NewY -= (Integer.Parse(FlickNaviPanelPanel.Height) - Integer.Parse(MainButtons(FlickingButton).Height)) / 2
        ElseIf System.Text.RegularExpressions.Regex.Match(FlickingButton.ToString, "^(11|15)$").Value <> "" Then
            NewY -= Integer.Parse(FlickNaviPanelPanel.Height) - Integer.Parse(MainButtons(FlickingButton).Height)
        End If

        FlickNaviPanelPanel.Location = New Point(NewX, NewY)


        Select Case mode
            Case 0
                If mode2 = 0 Then
                    NewButtonText = Hiragana(i).ToString.ToCharArray
                ElseIf mode2 = 1 Then
                    NewButtonText = HiraganaDakuHalfSmall(i).ToString.ToCharArray
                End If
            Case 1
                If mode2 = 0 Then
                    NewButtonText = Katakana(i).ToString.ToCharArray
                ElseIf mode2 = 1 Then
                    NewButtonText = KatakanaDakuHalfSmall(i).ToString.ToCharArray
                End If
            Case 2
                If mode2 = 0 Then
                    NewButtonText = Alphabet(i).ToString.ToCharArray
                ElseIf mode2 = 1 Then
                    NewButtonText = AlphabetSmall(i).ToString.ToCharArray
                End If
        End Select

        SubButtons(4).BackColor = Color.DeepSkyBlue

        SubButtons(4).Text = NewButtonText(0)
        SubButtons(1).Text = NewButtonText(1)
        SubButtons(3).Text = NewButtonText(2)
        SubButtons(7).Text = NewButtonText(3)
        SubButtons(5).Text = NewButtonText(4)



    End Sub

    Private Sub Flicking(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        If IsMouseDown = 0 Then
            Exit Sub
        End If


        Dim rect As Rectangle
        Dim MousePosition As Point

        Dim IsSelected = 0

        If MainButtons(FlickingButton).Text = "　" Then
            Exit Sub
        End If

        rect = MainButtons(FlickingButton).ClientRectangle
        MousePosition = MainButtons(FlickingButton).PointToClient(Control.MousePosition)
        If rect.Contains(MousePosition) Then
            SubButtons(1).BackColor = Color.White
            SubButtons(3).BackColor = Color.White
            SubButtons(5).BackColor = Color.White
            SubButtons(7).BackColor = Color.White
            SubButtons(4).BackColor = Color.DeepSkyBlue
        Else
            FlickNaviPanelPanel.Visible = True
            '
            'ここから3番判定
            If SubButtons(3).Text <> "　" Then
                If Me.PointToClient(System.Windows.Forms.Cursor.Position).Y < MainButtons(FlickingButton).Location.Y Then
                    If Me.PointToClient(System.Windows.Forms.Cursor.Position).X > MainButtons(FlickingButton).Location.X Then
                        If Me.PointToClient(System.Windows.Forms.Cursor.Position).X < (MainButtons(FlickingButton).Location.X + MainButtons(FlickingButton).Width) Then
                            SubButtons(1).BackColor = Color.White
                            SubButtons(4).BackColor = Color.White
                            SubButtons(5).BackColor = Color.White
                            SubButtons(7).BackColor = Color.White
                            SubButtons(3).BackColor = Color.DeepSkyBlue
                        End If
                    End If
                End If
            End If
            '
            'ここから5番判定
            If SubButtons(5).Text <> "　" Then
                If Me.PointToClient(System.Windows.Forms.Cursor.Position).Y > (MainButtons(FlickingButton).Location.Y + MainButtons(FlickingButton).Height) Then
                    If Me.PointToClient(System.Windows.Forms.Cursor.Position).X > MainButtons(FlickingButton).Location.X Then
                        If Me.PointToClient(System.Windows.Forms.Cursor.Position).X < (MainButtons(FlickingButton).Location.X + MainButtons(FlickingButton).Width) Then
                            SubButtons(1).BackColor = Color.White
                            SubButtons(3).BackColor = Color.White
                            SubButtons(4).BackColor = Color.White
                            SubButtons(7).BackColor = Color.White
                            SubButtons(5).BackColor = Color.DeepSkyBlue
                        End If
                    End If
                End If
            End If
            '
            'ここから1番判定
            If SubButtons(1).Text <> "　" Then
                If Me.PointToClient(System.Windows.Forms.Cursor.Position).X < MainButtons(FlickingButton).Location.X Then
                    If Me.PointToClient(System.Windows.Forms.Cursor.Position).Y > MainButtons(FlickingButton).Location.Y Then
                        If Me.PointToClient(System.Windows.Forms.Cursor.Position).Y < (MainButtons(FlickingButton).Location.Y + MainButtons(FlickingButton).Height) Then
                            SubButtons(3).BackColor = Color.White
                            SubButtons(4).BackColor = Color.White
                            SubButtons(5).BackColor = Color.White
                            SubButtons(7).BackColor = Color.White
                            SubButtons(1).BackColor = Color.DeepSkyBlue
                        End If
                    End If
                End If
            End If
            '
            'ここから7番判定
            If SubButtons(7).Text <> "　" Then
                If Me.PointToClient(System.Windows.Forms.Cursor.Position).X > MainButtons(FlickingButton).Location.X Then
                    If Me.PointToClient(System.Windows.Forms.Cursor.Position).Y > MainButtons(FlickingButton).Location.Y Then
                        If Me.PointToClient(System.Windows.Forms.Cursor.Position).Y < (MainButtons(FlickingButton).Location.Y + MainButtons(FlickingButton).Height) Then
                            SubButtons(1).BackColor = Color.White
                            SubButtons(3).BackColor = Color.White
                            SubButtons(4).BackColor = Color.White
                            SubButtons(5).BackColor = Color.White
                            SubButtons(7).BackColor = Color.DeepSkyBlue
                        End If
                    End If
                End If
            End If
        End If

    End Sub

    Private Sub EnterFlick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)

        Dim SendChar As String = ""

        For i As Integer = 0 To 8
            If SubButtons(i).BackColor = Color.DeepSkyBlue Then
                SendChar = SubButtons(i).Text
            End If
        Next

        SendKeys.SendWait(SendChar)

        IsMouseDown = 0
        FlickNaviPanelPanel.Visible = False

        MainButtons(FlickingButton).ForeColor = Color.Black
        MainButtons(FlickingButton).BackColor = Color.White

    End Sub

    Private Sub FlickNaviPanel_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles FlickNaviPanel.MouseMove
        Flicking(sender, e)
    End Sub

End Class
