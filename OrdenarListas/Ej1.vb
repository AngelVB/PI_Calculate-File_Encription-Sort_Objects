Option Explicit On
Option Strict On

Public Class Ej1

    Public Sub New()

    End Sub

    Public Function OrdenarLista(ByVal Lista As List(Of Persona)) As List(Of Persona)
        Lista.Sort()
        Return Lista
    End Function

    Public Sub Listardatos(P1 As List(Of Persona))
        For Each P As Persona In P1
            Console.WriteLine("Nombre: {0}, Edad: {1}", P.Nombre, P.Edad)
        Next
        Console.WriteLine("Pulse una tecla para continuar.")
        Console.ReadKey()
    End Sub



End Class
