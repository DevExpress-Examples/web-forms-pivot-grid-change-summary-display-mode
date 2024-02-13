<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SummaryDisplayMode.DefaultForm" %>

<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v22.1, Version=22.1.13.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPivotGrid" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v22.1, Version=22.1.13.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table class="OptionsTable BottomMargin">
            <tr>
                <td colspan="3">
                    <dx:ASPxRadioButtonList ID="rgSummaryDisplayTypeGroups" runat="server" AutoPostBack="true"
                        RepeatDirection="Horizontal" OnSelectedIndexChanged="rgSummaryDisplayTypeGroups_SelectedIndexChanged"
                        border-borderstyle="None" SelectedIndex="0">
                        <Items>
                            <dx:ListEditItem Value="0" Text="Variation" Selected="True" />
                            <dx:ListEditItem Value="1" Text="Percentage" />
                            <dx:ListEditItem Value="2" Text="Rank" />
                        </Items>
                    </dx:ASPxRadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    Summary Display Type:
                </td>
                <td>
                    <dx:ASPxComboBox ID="ddlSummaryDisplayType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSummaryDisplayType_SelectedIndexChanged"
                        width="195" />
                </td>
            </tr>
            <tr>
                <td>
                    <dx:ASPxCheckBox runat="server" ID="cbShowRawValues" Text="Show Raw Values" AutoPostBack="True"
                        OnCheckedChanged="cbShowRawValues_CheckedChanged" />
                </td>
                <td>
                </td>
                <td>
                    <dx:ASPxCheckBox runat="server" ID="cbAllowCrossGroupVariation" Text="Allow Cross-Group Variation"
                        AutoPostBack="True" OnCheckedChanged="cbAllowCrossGroupVariation_CheckedChanged" />
                </td>
            </tr>
        </table>
        <dx:ASPxPivotGrid ID="pivotGrid" runat="server" width="100%">
            <OptionsView ShowFilterHeaders="false" HorizontalScrollBarMode="Auto" />
            <OptionsData DataProcessingEngine="Optimized" />
            <OptionsFilter NativeCheckBoxes="False" />
        </dx:ASPxPivotGrid>
        <input runat="server" id="cachedSourceDataFieldName" type="hidden" enableviewstate="true" />
         <asp:SqlDataSource ID="ProductReportsDataSource" runat="server"
             ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
             ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
             SelectCommand="SELECT * FROM [ProductReports]"></asp:SqlDataSource>
         <asp:SqlDataSource ID="SalesPersonsDataSource" runat="server"
             ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
             ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
             SelectCommand="SELECT * FROM [SalesPerson]"></asp:SqlDataSource>

    </div>
    </form>
</body>
</html>
