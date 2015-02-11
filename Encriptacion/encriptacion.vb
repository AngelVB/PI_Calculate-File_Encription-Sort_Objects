Option Explicit On
Option Strict On
Module Encriptacion

    Sub Main()
        Dim Opcion As String = ""
        Const FicheroEntrada As String = "Entrada.txt"
        Const FicheroEncrip As String = "TextoEncriptado.txt"
        Const FicheroEntradaEncrip As String = "EntradaEncriptada.txt"
        Const FicheroClaro As String = "TextoEnClaro.txt"
        Dim BytesLectura As Byte()

        While Not Opcion.Equals("0")

            Menu(Opcion)

            Select Case Opcion
                Case "1"
                    BytesLectura = LeerFichero(FicheroEntrada)
                    OperarConBytes(BytesLectura, 1)
                    EscribirFichero(BytesLectura, FicheroEncrip)
                Case "2"
                    BytesLectura = LeerFichero(FicheroEntradaEncrip)
                    OperarConBytes(BytesLectura, 2)
                    EscribirFichero(BytesLectura, FicheroClaro)
                Case "0"
                    Console.WriteLine("Hasta otra.")
                    Console.ReadKey()
                Case Else
                    Console.WriteLine("Introduzca opción correcta")
                    Console.ReadKey()
            End Select
        End While

    End Sub

    Public Sub Menu(ByRef Opcion As String)

        Console.Clear()

        Console.WriteLine("***********************************************")
        Console.WriteLine("* 1. Encriptar Entrada.txt                    *")
        Console.WriteLine("*                                             *")
        Console.WriteLine("* 2. Desencriptar EntradaEncriptada.txt       *")
        Console.WriteLine("*                                             *")
        Console.WriteLine("* 0. Salir                                    *")
        Console.WriteLine("***********************************************")
        Console.WriteLine()
        Console.Write("Seleccione una opción[1, 2 o 3]:")
        Opcion = Console.ReadLine()

    End Sub

    Public Function LeerFichero(Entrada As String) As Byte()
        Dim EntradaBytes As Byte()

        Try
            EntradaBytes = IO.File.ReadAllBytes(Entrada) 'Cargo todos los bytes del texto de entrada

            Return EntradaBytes

        Catch Ex As IO.FileNotFoundException 'Capturo excepcion si el archivo encriptado no existe
            Throw New IO.FileNotFoundException("El fichero " + Entrada + " no existe.")
        End Try

    End Function

    Public Sub OperarConBytes(EntradaBytes As Byte(), Operacion As Integer) ' Operación = 1 Encriptar, Operación = 2 Desencriptar
        Dim ContadorParaXor As Integer 'Utilizo este contador para evitar el error de XOR cuando tenemos textos con mas de 255 caracteres, ya que i sería mayor de 8 bytes.

        Select Case Operacion
            Case 1 'ENCRIPTAR
                For i = 0 To EntradaBytes.Length - 1 'Bucle de encriptación

                    If ContadorParaXor < 255 Then
                        ContadorParaXor = ContadorParaXor + 1
                    Else
                        ContadorParaXor = 0 'reseteo el contador para el XOR
                    End If

                    EntradaBytes(i) = CByte(EntradaBytes(i) >> i Mod 8 Or EntradaBytes(i) << (8 - i Mod 8)) 'Rotación de bytes. 
                    EntradaBytes(i) = CByte(EntradaBytes(i) Xor ContadorParaXor) 'Operación XOR
                Next

            Case 2 'DESENCRIPTAR
                For i = 0 To EntradaBytes.Length - 1 'Bucle de desencriptación

                    If ContadorParaXor < 255 Then
                        ContadorParaXor = ContadorParaXor + 1
                    Else
                        ContadorParaXor = 0 'reseteo el contador para el XOR
                    End If
                    EntradaBytes(i) = CByte(EntradaBytes(i) Xor ContadorParaXor) 'Operación XOR
                    EntradaBytes(i) = CByte(EntradaBytes(i) << i Mod 8 Or EntradaBytes(i) >> (8 - i Mod 8)) 'Rotación de bytes. 
                Next

            Case Else
                'esta excepción, en teoría, no saltará nunca, ya que yo soy el que controla qué parámetros envío cada vez.
                Throw New ArgumentException("Parámetro de Operación inválido, utiliza 1 para encriptar o 2 para desencriptar.")
        End Select

    End Sub

    Public Sub EscribirFichero(SalidaBytes As Byte(), Salida As String)


        Try
            'Cargo todos los bytes del texto de entrada
            IO.File.WriteAllBytes(Salida, SalidaBytes) ' Escribo todos los bytes en el fichero encriptado. Utilizando este método consigo encriptar caracteres especiales sin problemas.

            Console.WriteLine(" Fichero {0} escrito con éxito", Salida)
            Console.ReadKey()


        Catch Ex As UnauthorizedAccessException 'Capturo excepcion si el archivo en claro no permite escribir
            Throw New UnauthorizedAccessException("Imposible escribir en " + Salida + ". Posiblemente se encuentre protegido contra escritura.")
        End Try
    End Sub

End Module
