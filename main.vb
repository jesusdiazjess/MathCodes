Module MainModule
    Sub Main()
        Console.WriteLine("Binary Arithmetic Calculator")
        Console.WriteLine("---------------------------")

        Dim continueFlag As Boolean = True

        While continueFlag
            Dim binary1 As String = GetBinaryInput("Enter the first binary number: ")
            Dim binary2 As String = GetBinaryInput("Enter the second binary number: ")

            Dim operatorChoice As String = GetOperatorInput()

            Dim result As String = ""

            Select Case operatorChoice
                Case "+"
                    result = AddBinary(binary1, binary2)
                Case "-"
                    result = SubtractBinary(binary1, binary2)
                Case "*"
                    result = MultiplyBinary(binary1, binary2)
                Case "/"
                    result = DivideBinary(binary1, binary2)
            End Select

            Console.WriteLine("Result: " & result)
            Console.WriteLine()

            Console.WriteLine("Do you want to perform another binary arithmetic operation? (y/n)")
            Dim response As String = Console.ReadLine()

            If response.ToLower() <> "y" Then
                continueFlag = False
            End If

            Console.WriteLine()
        End While
    End Sub

    Function GetBinaryInput(ByVal prompt As String) As String
        Dim binary As String
        Dim validInput As Boolean = False

        Do
            Console.Write(prompt)
            binary = Console.ReadLine().Trim()

            ' Check if input is a valid binary number
            validInput = True
            For Each digit As Char In binary
                If digit <> "0"c AndAlso digit <> "1"c Then
                    validInput = False
                    Exit For
                End If
            Next

            If Not validInput Then
                Console.WriteLine("Invalid binary number. Please try again.")
            End If
        Loop While Not validInput

        Return binary
    End Function

    Function GetOperatorInput() As String
        Dim validInput As Boolean = False
        Dim operatorChoice As String = ""

        Do
            Console.Write("Enter the arithmetic operator (+, -, *, /): ")
            operatorChoice = Console.ReadLine().Trim()

            Select Case operatorChoice
                Case "+", "-", "*", "/"
                    validInput = True
                Case Else
                    Console.WriteLine("Invalid operator. Please try again.")
            End Select
        Loop While Not validInput

        Return operatorChoice
    End Function

    Function AddBinary(ByVal binary1 As String, ByVal binary2 As String) As String
        Dim sum As New System.Text.StringBuilder()

        Dim carry As Integer = 0
        Dim i As Integer = binary1.Length - 1
        Dim j As Integer = binary2.Length - 1

        While i >= 0 OrElse j >= 0 OrElse carry <> 0
            Dim digit1 As Integer = If(i >= 0, Integer.Parse(binary1(i).ToString()), 0)
            Dim digit2 As Integer = If(j >= 0, Integer.Parse(binary2(j).ToString()), 0)

            Dim tempSum As Integer = digit1 + digit2 + carry
            sum.Insert(0, (tempSum Mod 2).ToString())

            carry = tempSum \ 2

            i -= 1
            j -= 1
        End While

        Return sum.ToString()
    End Function

    Function SubtractBinary(ByVal binary1 As String, ByVal binary2 As String) As String
        Dim result As New System.Text.StringBuilder()

        Dim borrow As Integer = 0
        Dim i As Integer = binary1.Length - 1
        Dim j As Integer = binary2.Length - 1

        While i >= 0 OrElse j >= 0
            Dim digit1 As Integer = If(i >= 0, Integer.Parse(binary1(i).ToString()), 0)
            Dim digit2 As Integer = If(j >= 0, Integer.Parse(binary2(j).ToString()), 0)

            Dim tempDiff As Integer = digit1 - digit2 - borrow

            If tempDiff < 0 Then
                tempDiff += 2
                borrow = 1
            Else
                borrow = 0
            End If

            result.Insert(0, tempDiff.ToString())

            i -= 1
            j -= 1
        End While

        ' Remove leading zeros
        While result.Length > 1 AndAlso result(0) = "0"c
            result.Remove(0, 1)
        End While

        Return result.ToString()
    End Function

    Function MultiplyBinary(ByVal binary1 As String, ByVal binary2 As String) As String
        Dim result As String = "0"

        Dim i As Integer = binary2.Length - 1
        Dim position As Integer = 0

        While i >= 0
            Dim digit As Integer = Integer.Parse(binary2(i).ToString())

            If digit = 1 Then
                result = AddBinary(result, binary1.PadRight(binary1.Length + position, "0"))
            End If

            position += 1
            i -= 1
        End While

        Return result
    End Function

    Function DivideBinary(ByVal binary1 As String, ByVal binary2 As String) As String
        Dim dividend As String = binary1
        Dim divisor As String = binary2
        Dim quotient As String = "0"

        While dividend.Length >= divisor.Length
            Dim divisorPadded As String = divisor.PadRight(dividend.Length, "0")

            If dividend.CompareTo(divisorPadded) >= 0 Then
                Dim tempResult As String = "1" & New String("0"c, dividend.Length - divisorPadded.Length)
                quotient = AddBinary(quotient, tempResult)

                dividend = SubtractBinary(dividend, divisorPadded)
            Else
                dividend = dividend.PadRight(dividend.Length + 1, "0")
                quotient = quotient.PadRight(quotient.Length + 1, "0")
            End If
        End While

        Return quotient
    End Function
End Module