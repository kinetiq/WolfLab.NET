Imports WolfLab.Core.Configuration
Imports Z.Core.Extensions

Public Class ProjectParser
    Private ReadOnly Doc As XDocument

    Public Sub New(doc As XDocument)
        Me.Doc = doc
    End Sub

    Public Function Parse() As ProjectModel
        Dim Project = New ProjectModel

        Project.Name = Doc.<project>.@name.Coalesce("Unnamed Project")
        Project.CrunchMode = GetCrunchMode()
        Project.MinVillage = GetMinVillage()
        Project.MaxVillage = GetMaxVillage()
        Project.SampleSize = GetSampleSize()

        For Each ExperimentXml In Doc...<experiment>
            Dim Experiment = New ExperimentModel

            Experiment.Name = ExperimentXml.@name

            For Each VariableXml In ExperimentXml...<variable>
                Dim Name As String = VariableXml.@name
                Dim Value As String = VariableXml.@value

                ValidateVariable(Name, Value)
                Experiment.Variables.Add(Name, Value)
            Next

            Project.Experiments.Add(Experiment)
        Next

        Return Project
    End Function

    Private Sub ValidateVariable(key As String, value As String)
        if key Is nothing
            Throw New InvalidOperationException($"Found a variable that has no Name.")
        End If

        if value Is nothing
            Throw New InvalidOperationException($"Found a variable that has no Value.")
        End If

        value = value.ToLower()
        key = key.ToLower()

        Select Case key
            Case "LynchRule".ToLower()
                'TODO: use the enum directly to determine validity...
                'Dim x = [Enum].GetValues(GetType(LynchRules)).Cast(Of LynchRules)().ToList()

                If Not value.In("NoRules".ToLower(), "MustLynchFirstDay".ToLower(), "MustLynchAlways".ToLower(), "NeverLynchFirstDay".ToLower()) Then
                    Throw New InvalidOperationException($"Invalid value for {key}: {value}")
                End If
            Case "SeerCount".ToLower()              
                CheckNonNegativeInt32(key, value)
            Case "SeerPercentScannedThreshold".ToLower()
                CheckNonNegativeDecimal(key, value)
            Case "SeerWolfPercentThreshold".ToLower()
                CheckNonNegativeDecimal(key, value)
            Case "SeerLivingScanCountThreshold".ToLower()
                CheckNonNegativeDecimal(key, value)
            Case "SeerWolfCountThreshold".ToLower()
                CheckNonNegativeDecimal(key, value)
            Case "HunterCount".ToLower()
                CheckNonNegativeInt32(key, value)            
            Case Else
                Throw New InvalidOperationException("Unexpected Variable: " + key)
        End Select
    End Sub

    Private sub CheckNonNegativeInt32(key As string, value As String)
        If Not value.IsValidInt32()
            Throw New InvalidOperationException($"Variable {key} must be an Integer.")
        End If

        If value.ToInt32() < 0 
            Throw New InvalidOperationException($"Variable {key} must be zero or positive (Actual: {value})")
        End If
    End sub

    Private sub CheckNonNegativeDecimal(key As string, value As String)
        If Not value.IsValidDecimal()
            Throw New InvalidOperationException($"Variable {key} must be an Integer.")
        End If

        If value.ToDecimal() < 0 
            Throw New InvalidOperationException($"Variable {key} must be zero or positive (Actual: {value})")
        End If
    End sub



    Private Function GetCrunchMode() As Boolean
        Dim Value = Doc.<project>.@crunchMode
        Dim DefaultValue = False

        If Value Is Nothing Then
            Return DefaultValue
        End If

        If Value.IsValidBoolean() Then
            Return Value
        End If

        Throw New InvalidOperationException($"crunchMode must contain a boolean value. Actual: {Value}")
    End Function

    Private Function GetSampleSize() As Integer
        Return GetValue("sampleSize", Doc.<project>.@sampleSize, 1000, 1, 100000)
    End Function

    Private Function GetMinVillage() As Integer
        Return GetValue("minVillage", Doc.<project>.@minVillage, 7, 4, 20)
    End Function

    Private Function GetMaxVillage() As Integer
        Dim Min = GetMinVillage()
        Dim Max = GetValue("maxVillage", Doc.<project>.@maxVillage, 10, 4, 20)

        If Min > Max Then
            Throw New InvalidOperationException($"MinVillage ({Min}) is larger than MaxVillage ({Max}). This is not allowed.")
        End If

        Return Max
    End Function

    Private Function GetValue(fieldName As String, value As String, defaultValue As Integer, min As Integer, max As Integer) As Integer
        If value Is Nothing Then
            Return defaultValue
        End If

        If Not value.IsValidInt32() OrElse value < min OrElse value > max Then
            Throw New InvalidOperationException($"{fieldName} must be an integer between {min} and {max}. Actual: {value}")
        End If

        Return value
    End Function
End Class
