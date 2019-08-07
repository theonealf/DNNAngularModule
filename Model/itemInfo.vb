Imports System
Imports System.Web.Caching
Imports DotNetNuke.ComponentModel.DataAnnotations

Namespace Model
    <TableName("Angularmodule_Items")>
    <PrimaryKey("ItemId", AutoIncrement:=True)>
    <Cacheable("BBAngular_Items_", CacheItemPriority.[Default], 20)>
    <Scope("ModuleId")>
    Public Class ItemInfo
        Public Property ItemId As Integer
        <ColumnName("ItemName")>
        Public Property Title As String
        <ColumnName("ItemDescription")>
        Public Property Description As String
        Public Property AssignedUserId As Integer?
        <ReadOnlyColumn>
        Public Property AssignedUserName As String
        Public Property ModuleId As Integer
        Public Property Sort As Integer
        Public Property CreatedByUserId As Integer
        Public Property LastModifiedByUserId As Integer
        Public Property CreatedOnDate As DateTime
        Public Property LastModifiedOnDate As DateTime
    End Class
End Namespace