'Programmer: Sully Erkan-Rijos
'Class: CST -226

'Lab: 4

'Description: 
'This Program emulates 3 dice rolling. The roll button will simultaneously roll all 3 dice.
'once instructions are read, game can begin. User has 5 turns, with 3 rolls per turn.
'User can hold die if they check the respective check box. objective is to get 3 of a kind
'for 1 point. Dependong on score, a particular message will appear at the end of a game. 

Imports System.IO

Imports System.Net
Public Class Dicey_Dice_Form

    'Variables------------------------------------------------
    Dim randomObject As New Random()

    Dim die1 As Integer
    Dim die2 As Integer
    Dim die3 As Integer

    Dim die1Value As Integer
    Dim die2Value As Integer
    Dim die3Value As Integer



    'Constants
    Const FILE_PREFIX As String = "/Images/die"
    Const fILE_SUFFIX As String = ".png"


    'Counter(s)
    Dim instructionsRead As Integer = 0
    Dim turn As Integer = 1
    Dim roll As Integer = 0
    Dim point As Integer = 0
    Dim diceTotal As Integer = 0

    'Variables------------------------------------------------


    Private Sub Dicey_Dice_Form_Load(sender As Object, e As EventArgs) Handles Me.Load
        'initialize custom cursor
        Dim cur As Cursor = New Cursor(New IO.MemoryStream(My.Resources.One_die))

        'assign cursor
        If Not cur Is Nothing Then

            Me.Cursor = cur

        Else

            Me.Cursor = Cursors.Default

        End If

        rollBtn.Enabled = False
        resetBtn.Enabled = False
        endTurnBtn.Enabled = False
        instructionsBtn.Enabled = True

        'MessageBox.Show("User must read instructions by clicking instructions button first !")

    End Sub


    Private Sub instructionsBtn_Click(sender As Object, e As EventArgs) Handles instructionsBtn.Click

        instructionsRead = +1

        MessageBox.Show(" DICEY DICE GAME:  
Objective is to roll 3 of a kind. User has 5 turns, with 3 rolls per turn. Roll dice, if 3 of a kind is rolled = 1 point. 
User can hold dice, use HOLD checkbox to hold or un-hold die to make 3 of a kind line up and score a point. ")


        If instructionsRead = 1 Then

            rollBtn.Enabled = True

        End If

    End Sub

    Private Sub rollBtn_Click(sender As Object, e As EventArgs) Handles rollBtn.Click

        resetBtn.Enabled = True

        endTurnBtn.Enabled = True

        If turn <= 5 And roll <= 3 Then

            RollDice()

            'Else

            'reset()

            'MessageBox.Show("Maximum turns used. Game will now reset for new game.")

        End If

    End Sub


    Sub RollDice()

        If roll < 3 Then

            If die1HoldCheckBox.Checked = False Then
                die1 = randomObject.Next(1, 7)
            Else
                die1Value = die1
            End If

            '------

            If die2HoldCheckBox.Checked = False Then
                die2 = randomObject.Next(1, 7)
            Else
                die2Value = die2
            End If

            '------

            If die3HoldCheckBox.Checked = False Then
                die3 = randomObject.Next(1, 7)
            Else
                die3Value = die3
            End If
            '------

            GroupBox1.Text = "die1 = " & die1 & " - " & " die2 = " & die2 & " - " & " die3 = " & die3

            die1PictureBox.Visible = True
            die2PictureBox.Visible = True
            die3PictureBox.Visible = True

            DisplayDie(die1PictureBox, die1)
            DisplayDie(die2PictureBox, die2)
            DisplayDie(die3PictureBox, die3)

            roll += 1

        ElseIf turn = 5 And roll = 3 Then

            reset()

        ElseIf turn < 5 And roll = 3 Then

            turn += 1

            roll = 1

            MessageBox.Show("Next turn")

        End If

        turnLbl.Text = "Turn: " & CStr(turn) & " of 5"

        rollLbl.Text = "Roll: " & CStr(roll) & " of 3"

        pointLbl.Text = "Point(s): " & CStr(point)

        pointAssigner()

    End Sub

    Sub DisplayDie(ByVal die As PictureBox, ByVal face As Integer)

        'assign value to face

        'assign die images to picture box
        die.Image = Image.FromFile(Directory.GetCurrentDirectory & FILE_PREFIX & face & fILE_SUFFIX)

    End Sub

    Private Sub Reset_Click(sender As Object, e As EventArgs) Handles resetBtn.Click

        Dim messageResult As DialogResult = MessageBox.Show("Are you sure you want to reset the game? Click OK to reset, Cancel to continue.",
                                                            "Reset Game", MessageBoxButtons.OKCancel)

        If messageResult = DialogResult.OK Then

            reset()

        End If
    End Sub


    Private Sub endTurnBtn_Click(sender As Object, e As EventArgs) Handles endTurnBtn.Click

        If turn < 5 Then

            pointAssigner()

            roll = 1

            turn += 1

            turnLbl.Text = "Turn: " & CStr(turn) & " of 5"
            rollLbl.Text = "Roll: " & CStr(roll) & " of 3"
            pointLbl.Text = "Point(s): " & CStr(point)

            MessageBox.Show("Next turn")

        ElseIf turn = 5 And roll >= 1 Then

            endTurnBtn.Enabled = False

            reset()

        End If

    End Sub

    Sub reset()

        pointMessage()

        turn = 1
        roll = 1
        point = 0

        die1 = randomObject.Next(1, 7)
        die2 = randomObject.Next(1, 7)
        die3 = randomObject.Next(1, 7)

        rollBtn.Enabled = True
        endTurnBtn.Enabled = False
        resetBtn.Enabled = False

        turnLbl.Text = "Turn: " & CStr(turn) & " of 5"
        rollLbl.Text = "Roll: " & CStr(roll) & " of 3"
        pointLbl.Text = "Point(s): " & CStr(point)

        GroupBox1.Text = "die1 = " & " - " & " die2 = " & " - " & " die3 = "

        die1PictureBox.Visible = Nothing
        die2PictureBox.Visible = Nothing
        die3PictureBox.Visible = Nothing

        die1HoldCheckBox.Checked = False
        die2HoldCheckBox.Checked = False
        die3HoldCheckBox.Checked = False

        MessageBox.Show("Game Reset !")

    End Sub

    Sub pointAssigner()

        If (die1 = die1Value) And (die2 = die2Value) And (die3 = die3Value) Then

            point += 1

            MessageBox.Show("SCORE 1 POINT !")

            die1HoldCheckBox.Checked = False
            die2HoldCheckBox.Checked = False
            die3HoldCheckBox.Checked = False

            die1 = randomObject.Next(1, 7)
            die2 = randomObject.Next(1, 7)
            die3 = randomObject.Next(1, 7)

            GroupBox1.Text = "die1 = " & die1 & " - " & " die2 = " & die2 & " - " & " die3 = " & die3

        ElseIf die1 = die2 And die2 = die3 Then

            point += 1

            MessageBox.Show("1 POINT on 1 Roll, LUCKY !")

            die1HoldCheckBox.Checked = False
            die2HoldCheckBox.Checked = False
            die3HoldCheckBox.Checked = False

            die1 = randomObject.Next(1, 7)
            die2 = randomObject.Next(1, 7)
            die3 = randomObject.Next(1, 7)

            GroupBox1.Text = "die1 = " & die1 & " - " & " die2 = " & die2 & " - " & " die3 = " & die3

        End If

        turnLbl.Text = "Turn: " & CStr(turn) & " of 5"
        rollLbl.Text = "Roll: " & CStr(roll) & " of 3"
        pointLbl.Text = "Point(s): " & CStr(point)

        DisplayDie(die1PictureBox, die1)
        DisplayDie(die2PictureBox, die2)
        DisplayDie(die3PictureBox, die3)

    End Sub

    Sub pointMessage()

        Select Case point
            Case 0
                MessageBox.Show("0 points, you have done poorly.")

            Case 1
                MessageBox.Show("One point is better than none.")

            Case 2, 3
                MessageBox.Show("2 or 3 points is Mediocre at best.")

            Case 4
                MessageBox.Show("4 points ! Almost Perfect.")

            Case 5
                MessageBox.Show("Perfect Score !")

                specialEvent()

        End Select
    End Sub

    Sub specialEvent()

        Dim diceVideo As New WebClient()

        Try

            MessageBox.Show("Opening https://www.youtube.com/watch?v=9L-VhUmir-A in the default web browser.")

            Process.Start("https://www.youtube.com/watch?v=9L-VhUmir-A")

        Catch ex As Exception

            MessageBox.Show($"Error opening the website: {ex.Message}")
        End Try

    End Sub



    'MENU ITEMS ---------------------------------
    Private Sub changeFormFontMenuItem_Click(sender As Object, e As EventArgs) Handles changeFormFontMenuItem.Click

        Dim fontDialog As New FontDialog()

        If fontDialog.ShowDialog() = DialogResult.OK Then
            Me.Font = fontDialog.Font
        End If

    End Sub

    Private Sub changeButtonColorMenuItem_Click(sender As Object, e As EventArgs) Handles changeButtonColorMenuItem.Click

        Dim colorDialog As New ColorDialog()

        If colorDialog.ShowDialog() = DialogResult.OK Then
            rollBtn.BackColor = colorDialog.Color
        End If

    End Sub

    Private Sub changeFormBackgroundMenuItem_Click(sender As Object, e As EventArgs) Handles changeFormBackGroundMenuItem.Click

        Dim colorDialog As New ColorDialog()

        If colorDialog.ShowDialog() = DialogResult.OK Then
            Me.BackColor = colorDialog.Color
        End If

    End Sub

    Private Sub displaySpecialMenuItem_Click(sender As Object, e As EventArgs) Handles displaySpecialMenuItem.Click

        specialEvent()

    End Sub


End Class
