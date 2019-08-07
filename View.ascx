﻿<%@ Control Language="vb" AutoEventWireup="true" CodeBehind="View.ascx.vb" Inherits="frontdata.aj.AJ_angularModule2.View" %>
<%@ Register TagPrefix="dnn" TagName="JavaScriptLibraryInclude" Src="~/admin/Skins/JavaScriptLibraryInclude.ascx" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement" Assembly="DotNetNuke.Web.Client" %>

<dnn:JavaScriptLibraryInclude runat="server" Name="AngularJS" />
<dnn:JavaScriptLibraryInclude runat="server" Name="angular-route" />
<dnn:JavaScriptLibraryInclude runat="server" Name="angular-ng-progress" />
<dnn:DnnCssInclude runat="server" FilePath="~/Resources/libraries/angular-ng-progress/01_00_07/ngProgress.min.css" />
<dnn:JavaScriptLibraryInclude runat="server" Name="angular-ui-sortable" />
<dnn:JavaScriptLibraryInclude runat="server" Name="angular-ng-dialog" />
<dnn:DnnCssInclude runat="server" FilePath="~/Resources/libraries/angular-ng-dialog/00_05_01/ngDialog.min.css" />
<dnn:DnnCssInclude runat="server" FilePath="~/Resources/libraries/angular-ng-dialog/00_05_01/ngDialog-theme-default.min.css" />


<div id="itemApp<%=ModuleId%>" class="itemApp">
    <div ng-view>Loading...Angular</div>
</div>

<script>
    angular.element(document).ready(function () {

        function init(appName, moduleId, apiPath) {
            var sf = $.ServicesFramework(moduleId);
            var localAppName = appName + moduleId;
            var application = angular.module(localAppName, [appName])
                .constant("serviceRoot", sf.getServiceRoot(apiPath))
                .config(function($httpProvider) {
                    var httpHeaders = { "ModuleId": sf.getModuleId(), "TabId": sf.getTabId(), "RequestVerificationToken": sf.getAntiForgeryValue() };
                    angular.extend($httpProvider.defaults.headers.common, httpHeaders);
                });
            return application;
        };

        var app = init("itemApp", <%=ModuleId%>, "AJ_Angularmodule2");
        app.constant("userlist", "<%=Users%>");
        app.constant("resources", "<%=Resources%>");
        app.constant("editable", "<%=Editable%>");
        app.constant("moduleId", "<%=ModuleId%>");
        app.constant("settings", "<%=ModuleSettings%>");
        var moduleContainer = document.getElementById("itemApp<%=ModuleId%>");
        angular.bootstrap(moduleContainer, [app.name]);
    });
</script>