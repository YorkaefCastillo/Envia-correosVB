Imports System.Net
Imports System.Net.Mail
Imports System.Data
Imports MySql.Data.MySqlClient
Imports System

Module Program
    Dim DB_Source As String = "____________"
    Dim DB_Port As String = "____________"
    Dim DB_Nombre As String = "UPANADB"
    Dim DB_Id As String = "__________"
    Dim DB_Pass As String = "_______________"

    Public Sub EnviarCorreos()
        'Base de Datos

        Dim Conn As New MySqlConnection("Database=" + DB_Nombre _
                                   + ";Data Source=" + DB_Source _
                                   + ";Port=" + DB_Port _
                                   + ";User Id=" + DB_Id _
                                   + ";Password=" + DB_Pass _
                                   + ";sqlservermode=True;")
        Conn.Open()
        '******************************************************************************************************
        Console.WriteLine("*****************************")
        Console.WriteLine(" ***** ENVIO DE CORREOS *****")
        Console.WriteLine("*****************************")
        Console.WriteLine()
        Dim Cant As Integer
        Console.WriteLine("***    Cantidad de Clientes    ***")
        Console.WriteLine("*** Que desea Enviar el Correo ***")
        Cant = Console.ReadLine


        Dim Mess As String
        Dim Mensaje As String
        Dim Asunto As String

        Console.WriteLine(" *** ASUNTO ***")
        Asunto = Console.ReadLine
        Console.Write(vbNewLine)
        Console.WriteLine(" *** MENSAJE *** ")
        Mess = Console.ReadLine

        For j = 1 To Cant Step 1

            Try
                Dim query As String = "select Email from Clientes where IdCliente =" & j & ";"
                Dim da As New MySqlDataAdapter(query, Conn)
                Dim ds As New DataSet()
                da.Fill(ds, "Clientes")
                Dim dt As DataTable = ds.Tables("Clientes")

                '******************************Correos****************************************************************

                For Each row As DataRow In dt.Rows
                    For Each col As DataColumn In dt.Columns
                        Dim M As String = row(col).ToString()


                        Dim message As New MailMessage
                        Dim smtp As New SmtpClient

                        '****************************************************************************************************


                        Mensaje = "Hola " & M & " " & Mess

                        With message
                            .From = New System.Net.Mail.MailAddress("ranfery2022@gmail.com")
                            .To.Add(M)
                            .Body = Mensaje
                            .Subject = Asunto
                            .Priority = System.Net.Mail.MailPriority.Normal
                        End With

                        With smtp
                            .EnableSsl = True
                            .Port = "587"
                            .Host = "smtp.gmail.com"
                            .Credentials = New Net.NetworkCredential("ranfery2022@gmail.com", "xkdovoybaebutbus")
                            .Send(message)

                        End With

                        Try
                            Console.Write("Correo Enviado A: " & M)
                        Catch ex As Exception
                            Console.Write("Error " & ex.Message & " al Enviar el Correo")
                        End Try
                        Console.Write(vbNewLine)
                        '***************************************Correo****************************************************

                    Next

                    Console.Write(vbNewLine)
                Next

            Catch e As Exception
                Console.WriteLine("Error: {0}", e.ToString())
            Finally
                If Conn IsNot Nothing Then
                    Conn.Close()
                End If
            End Try
            '************************************************************************************************************************
        Next
    End Sub

    Public Sub DB()
        Dim Conn As New MySqlConnection("Database=" + DB_Nombre _
                                   + ";Data Source=" + DB_Source _
                                   + ";Port=" + DB_Port _
                                   + ";User Id=" + DB_Id _
                                   + ";Password=" + DB_Pass _
                                   + ";sqlservermode=True;")
        Conn.Open()

        Try
            Dim query As String = "SELECT * FROM Clientes ;"
            Dim da As New MySqlDataAdapter(query, Conn)
            Dim ds As New DataSet()
            da.Fill(ds, "Clientes")
            Dim dt As DataTable = ds.Tables("Clientes")
            Console.WriteLine("************************************ Base de Datos De Clientes ************************************")
            Console.WriteLine()
            Console.WriteLine("___________________________________________________________________________________________________")

            For Each row As DataRow In dt.Rows
                For Each col As DataColumn In dt.Columns
                    Console.Write(row(col).ToString() + vbTab + "|| ")
                Next
                Console.Write(vbNewLine)
            Next
            Console.WriteLine("___________________________________________________________________________________________________")
            Console.Write(vbNewLine)



        Catch e As Exception
            Console.WriteLine("Error: {0}", e.ToString())
        Finally
            If Conn IsNot Nothing Then
                Conn.Close()
            End If
        End Try


    End Sub

    Sub Main(args As String())

        Dim Op As Integer


        Do
            Console.WriteLine("***************************************")
            Console.WriteLine("************ Threads Pool *************")
            Console.WriteLine("***************************************")


            Console.WriteLine("** Presione 1: Base De Datos Clientes **")
            Console.WriteLine("** Presione 2: Enviar Correo          **")
            Console.WriteLine("** Presione 3: Limpiar Pantalla       **")
            Console.WriteLine("** Presione 4: Salir                  **")
            Console.WriteLine("########## Que Opcion Desea ############")
            Op = Console.ReadLine


            Select Case Op
                Case 1
                    Console.Clear()
                    DB()
                Case 2
                    Console.Clear()
                    EnviarCorreos()
                Case 3
                    Console.Clear()
                Case 4
                    Console.Clear()
                    Console.WriteLine("******************************")
                    Console.WriteLine("***  Que Tenga Un Buen Dia ***")
                    Console.WriteLine("******************************")
                    Console.WriteLine("***     Presione Enter     ***")
                    Console.WriteLine("******************************")
                    Console.ReadKey()
                    End

            End Select

        Loop While (Op)



    End Sub

End Module
