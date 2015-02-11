Option Explicit On
Option Strict On

Module OrdenarListas

    Sub Main()
        Dim Ej1 As Ej1 = New Ej1()

        Dim Number As String = ""
        Dim Lista As List(Of Persona) = New List(Of Persona)

        While Number <> "0"
            Console.Clear()
            Console.WriteLine("**************************************")
            Console.WriteLine("*   1.Añadir Persona                 *")
            Console.WriteLine("*                                    *")
            Console.WriteLine("*   2. Ordenar Lista                 *")
            Console.WriteLine("*                                    *")
            Console.WriteLine("*   3. Listar Datos                  *")
            Console.WriteLine("*                                    *")
            Console.WriteLine("*   0. Salir                         *")
            Console.WriteLine("**************************************")
            Console.WriteLine()
            Console.Write("Seleccione una opción[1 o 2]:")
            Number = Console.ReadLine()

            Select Case Number
                Case "1"
                    Dim Nombre As String
                    Dim Edad As UInteger
                    Console.Write("Introduzca el nombre : ")
                    Nombre = Console.ReadLine
                    Console.Write("Introduzca la edad :")
                    Edad = CUInt(Console.ReadLine())
                    
                    Dim Pers As Persona = New Persona(Nombre, Edad)

                    
                    Lista.Add(Pers)
                Case "2"
                    Lista = Ej1.OrdenarLista(Lista)
                Case "3"
                    Ej1.Listardatos(Lista)
                Case "0"
                    Console.WriteLine("Hasta otra.")
                    Console.ReadKey()
                Case Else
                    Debug.WriteLine("Introduzca opción correcta")
            End Select
        End While
        Console.ReadLine()
    End Sub
End Module