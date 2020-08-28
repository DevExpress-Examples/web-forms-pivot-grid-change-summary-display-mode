<%@ Page Language="vb" AutoEventWireup="true" CodeBehind="Default.aspx.vb" Inherits="SummaryDisplayMode.DefaultForm" %>

<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v17.2, Version=17.2.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPivotGrid" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
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
                            <dx:ListEditItem Value="3" Text="Index" />
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
                <td>
                    <dx:ASPxCheckBox runat="server" ID="cbHideEmptyVariationItems" Text="Hide Empty Variation Items"
                        AutoPostBack="True" OnCheckedChanged="cbHideEmptyVariationItems_CheckedChanged" />
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
            <OptionsData DataProcessingEngine="LegacyOptimized" />
            <OptionsFilter NativeCheckBoxes="False" />
        </dx:ASPxPivotGrid>
        <input runat="server" id="cachedSourceDataFieldName" type="hidden" enableviewstate="true" />
        <asp:AccessDataSource ID="SalesPersonsDataSource" runat="server" DataFile="~/App_Data/nwind.mdb"
            SelectCommand="SELECT * FROM [SalesPerson]"></asp:AccessDataSource>
        <asp:AccessDataSource ID="ProductReportsDataSource" runat="server" DataFile="~/App_Data/nwind.mdb"
            SelectCommand="SELECT * FROM [ProductReports]"></asp:AccessDataSource>
    </div>
    </form>
</body>
</html>