Option Explicit On
Option Strict On
Module Pi
    Sub Main()
        'Dim Positivo As Boolean

        Dim NumeroPi As Decimal
        Dim Metodo As Char
        Metodo = CChar(" ")
        Dim NumTerminos As Integer

        While Not Metodo.Equals(CChar("0"))

            'El año pasado realizamos un ejercicio parecido, pero con otro método de aproximación. Ya que lo tenía
            'en C#, empecé por traducirlo y cuando lo tuve funcionando, implementé el método del ejercicio, así que
            'incluyo los dos.
            Metodo = SeleccionarMetodo()

            If Not Metodo.Equals(CChar("0")) Then

                IntroducirTerminos(NumTerminos)
                NumeroPi = CalcularPi(Metodo, NumTerminos)

                Console.WriteLine("")
                Console.WriteLine("Valor final de PI= {0}", NumeroPi) ' Valor final del PI.
                Console.WriteLine("")
                Console.WriteLine("Pulse 0 para salir o cualquier otra tecla para volver a calcular PI.")

                Metodo = Console.ReadKey(True).KeyChar
            End If


        End While
        Console.WriteLine("")
        Console.WriteLine("Hasta otra.")
        Console.ReadKey()


    End Sub



    Public Function SeleccionarMetodo() As Char
        Dim Metodo As Char
        Metodo = CChar(" ")

        While Not (Metodo.Equals(CChar("1"))) And Not (Metodo.Equals(CChar("2"))) And Not (Metodo.Equals(CChar("0")))
            Console.Clear()
            Console.WriteLine("Seleccione el método para calcular PI o 0 para salir:")
            Console.WriteLine()
            Console.WriteLine("   1       Método DAM-2014.")
            Console.WriteLine("   2       Método DAM-2013.")
            Console.WriteLine("")
            Console.WriteLine("   0       Salir.")
            Metodo = Console.ReadKey(True).KeyChar

            If Not (Metodo.Equals(CChar("1"))) And Not (Metodo.Equals(CChar("2"))) And Not (Metodo.Equals(CChar("0"))) Then
                Console.WriteLine("Opción inválida. Pulse cualquier tecla par continuar.")
                Console.ReadKey()

            End If
        End While

        Return Metodo
    End Function

    Public Sub IntroducirTerminos(ByRef NumTerminos As Integer)

        Try
            Console.Clear()
            Console.Write("Introduzca número de términos a utilizar:")
            NumTerminos = CInt(Console.ReadLine())
            If NumTerminos < 2 Then 'Controlo que el número de términos sea mayor de 1 
                Throw New ArgumentException("Ha introducido un número menor de 2")
            End If
        Catch Ex As InvalidCastException ' Capturo el caso de que hayan dejado la variable en blanco.
            Console.WriteLine("Introduzca un número válido.")
            Console.ReadKey()
            IntroducirTerminos(NumTerminos)
        Catch Ex As ArgumentException
            Console.WriteLine("Introduzca un número mayor de 1.")
            Console.ReadKey()
            IntroducirTerminos(NumTerminos)
        End Try
    End Sub
    Public Function CalcularPi(ByVal Metodo As String, ByVal NumTerminos As Integer) As Decimal
        Dim Indice As Integer
        Dim Numerador As Integer
        Dim Denominador As Integer
        Dim NumeroPi As Decimal
        NumeroPi = 4
        Console.WriteLine("")
        Select Case Metodo
            Case "1" 'MÉTODO 2014
                Numerador = 2
                Denominador = 3
                For Indice = 1 To NumTerminos 'inicio del bucle para cálculo de PI, parará al llegar al número de términos
                    If Indice = Numerador Then
                        Numerador = Numerador + 2
                    End If
                    If Indice = Denominador Then
                        Denominador = Denominador + 2
                    End If
                    NumeroPi = CDec(NumeroPi * (Numerador / Denominador))
                    Console.WriteLine("Término nº {1} -- Pi={0}", NumeroPi, Indice) 'Con esta línea muestro, a modo de curiosidad todos los valores que va adquiriendo PI
                Next
            Case "2" 'MÉTODO 2013
                Dim Positivo As Boolean

                Positivo = True 'variable para controlar si tengo que sumar o restar el término

                For Indice = 1 To NumTerminos 'inicio del bucle para cálculo de PI, parará al llegar al número de términos
                    If Positivo = True Then
                        NumeroPi = CDec(NumeroPi + (1 / (Indice + (Indice - 1))))
                        Positivo = False
                    Else
                        NumeroPi = CDec(NumeroPi - (1 / (Indice + (Indice - 1))))
                        Positivo = True

                    End If
                    Console.WriteLine("Término nº {1} -- Pi={0}", NumeroPi, Indice) 'Con esta línea muestro, a modo de curiosidad todos los valores que va adquiriendo PI
                Next
            Case Else
                Console.WriteLine("Introduzca un método válido")
                Console.ReadKey()
                CalcularPi(Metodo, NumTerminos)
        End Select
        Return NumeroPi
    End Function
End Module
