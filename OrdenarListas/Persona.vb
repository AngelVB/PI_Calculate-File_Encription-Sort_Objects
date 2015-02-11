Option Explicit On
Option Strict On
Public Class Persona
    Implements IComparable

    Private _Nombre As String
    Private _Edad As UInteger

    Public Property Nombre() As String
        Get
            Return _Nombre
        End Get
        Set(ByVal N As String)
            If (N = "") Then
                Throw New ArgumentException("No ha introducido nombre.")
            End If
            _Nombre = N
        End Set
    End Property

    Public Property Edad() As UInteger
        Get
            Return _Edad
        End Get
        Set(ByVal E As UInteger)
            _Edad = E
        End Set
    End Property

    Public Sub New(ByVal Nombre As String, ByVal Edad As UInteger)
        Me.Nombre = Nombre
        Me.Edad = Edad
    End Sub

    Public Sub New()
    End Sub

    Public Function CompareTo(Obj As Object) As Integer Implements IComparable.CompareTo
        Dim P As Persona = TryCast(Obj, Persona)
        If P Is Nothing Then
            Throw New Exception("El objeto no es una persona")
        End If

        Dim Nombre1 As String = StrConv(Nombre, VbStrConv.Uppercase)
        Dim Nombre2 As String = StrConv(P.Nombre, VbStrConv.Uppercase)
        If Edad > P.Edad Then
            Return -1
        ElseIf Edad < P.Edad Then
            Return 1
        Else
            If Nombre1 > Nombre2 Then
                Return 1
            ElseIf Nombre1 < Nombre2 Then
                Return -1
            Else
                Return 0
            End If
        End If
    End Function

    Public Overrides Function Equals(Obj As Object) As Boolean
        Dim X As Persona = TryCast(Obj, Persona)

        Return (X IsNot Nothing) And (Me.Edad.Equals(X.Edad)) And (Me.Nombre.Equals(X.Nombre))
    End Function

    Public Overrides Function GetHashCode() As Integer
        Return Me.Nombre.GetHashCode() Xor Me.Edad.GetHashCode
    End Function


    Protected Overrides Sub Finalize()
        _Nombre = ""
        _Edad = 0
        MyBase.Finalize()
    End Sub

End Class
