# database scripts

================================================================================================
EXECUTE BELOW SCRIPT IN	SQL SERVER AND CHANGE CONNECTION-STRING INSIDE "appsettings.json" FILE.
================================================================================================

CREATE DATABASE [PharmacyAdmin]

GO
USE [PharmacyAdmin]
GO

CREATE TABLE [dbo].[Article](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](250) NOT NULL,
 CONSTRAINT [PK_Article] PRIMARY KEY CLUSTERED 
	(
	[ID] ASC
	)
)

INSERT [dbo].[Article] ([Title]) VALUES (N'The Code Hubs')
GO
INSERT [dbo].[Article] ([Title]) VALUES (N'Faisal')
GO
INSERT [dbo].[Article] ([Title]) VALUES (N'Sort Table')
GO
INSERT [dbo].[Article] ([Title]) VALUES (N'Abc')
GO
INSERT [dbo].[Article] ([Title]) VALUES (N'Test Article')
GO
INSERT [dbo].[Article] ([Title]) VALUES (N'C# Corner')
GO

CREATE PROCEDURE [dbo].[SP_Add_Article]    
    @Title NVARCHAR(250) 
AS    
    BEGIN    
 DECLARE @Id as BIGINT  
        INSERT  INTO [Article]    
                (Title
             )    
        VALUES  ( @Title       
             );   
		SET @Id = SCOPE_IDENTITY();   
        SELECT  @Id AS ArticleID;    
    END;   
GO	
	
CREATE PROCEDURE [dbo].[SP_Update_Article] 
		@Id INT,   
		@Title NVARCHAR(250)   
	AS    
		BEGIN    

		UPDATE [Article] SET Title = @Title WHERE ID = @Id 
	           
		END;    

# Test Data
Audit ID518-1Q2021
547099,553390,559541,559628,560174,561232,561383,562892,562893,563993,565422,565424
StartDate = 1/17/2020
EndDate = 12/14/2020


# IWebHostingEnvironment
This code snippet can be used in the DownloadController.cs
string webRootPath = _webHostEnvironment.WebRootPath;          // C:\global\PharmacyAdmin\PharmacyAdmin\wwwroot
string contentRootPath = _webHostEnvironment.ContentRootPath;  // C:\global\PharmacyAdmin\PharmacyAdmin

string path = "";
path = Path.Combine(webRootPath, "CSS");
//or path = Path.Combine(contentRootPath , "wwwroot" ,"CSS" );

# Snipped for http error codes
            if (_DocumentManagementService.State.ErrorOccurred)
            {
                // return NoContent(); // HTTP 204 - The server sucessfully processed the request but there is nothing to return.
                // return BadRequest(); // HTTP 400 - Bad Request
                // return Unauthorized(); // HTTP 401 - Not authenticated
                return NotFound("HTTP Code 204 - The server sucessfully processed the request but the file is not available."); // HTTP 404 
                // return File(memory, "application/octet-stream", DocumentDownloadName); // This will return the 204 file
            }
# Local Temp Folder Testing mode

Set Both the values below to Test.

    "Mode": "Test",
    "DocumentStorageMode": "Test",

Set both the values below to Prod to run in Production with file server storage located on ISBKOFPROD01
    "Mode": "Prod",
    "DocumentStorageMode": "Prod",

# FedEx Search Screen
                                @if (@entity.DocumentDownloaded == 0)
                                {
                                    <td>Doc Expired</td>
                                }
                                else if (string.IsNullOrEmpty(@entity.Code) || (@entity.Code == "0")
                                {
                                    <td></td>
                                }
                                else if (string.IsNullOrEmpty(@entity.Code) || (@entity.Code == "0")
                                {
                                    <td></td>
                                }

# Grid

 <TelerikGrid Data="@Entities" AutoGenerateColumns="false" Pageable="true" PageSize="4">
        <GridColumns>
            <GridColumn Field="ncpdp_rx_number" />
            <GridColumn Field="@nameof(ProfitabilityNCPDPModel.ncpdp_rx_number)" Title="Rx Number" Width="195px" />
        </GridColumns>
 </TelerikGrid>

  <TelerikGrid Data="@Entities" AutoGenerateColumns="false" Pageable="true" PageSize="4">
        <GridColumns>
            <GridColumn Field="ncpdp_rx_number" Title="RX NO" Width="195px" />
            <GridColumn Field="ncpdp_invoice_number" Title="Invoice No" Width="195px" />
            <GridColumn Field="ncpdp_date_filled_timestamp?.ToShortDateString()" Title="Date Filled" Width="195px" />
            <GridColumn Field="TICKNO" Title="Ticket No" Width="195px" />
            <GridColumn Field="ticket_confirmation_date?.ToShortDateString()" Title="Ticket Conf. Date" Width="195px" />
            <GridColumn Field="ncpdp_quantity_new_refill_code" Title="Refill No" Width="195px" />
            <GridColumn Field="ticket_quantity" Title="Quantity" Width="195px" />
            <GridColumn Field="ncpdp_drug_name" Title="Drug Name" Width="195px" />
            <GridColumn Field="Copay" Title="Copay" Width="195px" />
            <GridColumn Field="TPRevenuePlusPatientCopay" Title="TP Revenue + Patient Copay" Width="195px" />
            <GridColumn Field="COGS" Title="COGS" Width="195px" />
            <GridColumn Field="COGSAdjusted" Title="COGS Adjusted" Width="195px" />
            <GridColumn Field="GrossProfit" Title="Gross Profit" Width="195px" />
            <GridColumn Field="GrossProfitAdjusted" Title="Gross Profit Adjusted" Width="195px" />
        </GridColumns>
    </TelerikGrid>

<TelerikGrid Data="@Entities" AutoGenerateColumns="false" Pageable="true" PageSize="50" SelectionMode="@GridSelectionMode.Single" @bind-SelectedItems="@SelectedEntities" 
                 SelectedItemsChanged="@((IEnumerable<ProfitabilityNCPDPModel> strategicItems) => OnStrategicSelectAsync(strategicItems))"
                  >

<GridColumn Field="@(nameof(ProfitabilityNCPDPModel.TicketSelected))" Title="Ticket Selected">
    <Template>
       Ticket selected is: 
    <br />
    @((context as ProfitabilityNCPDPModel).TicketSelected)
</Template>
</GridColumn>

# Grid Colors
<style>
    .custom-row-colors .k-grid-table .k-master-row {
        background-color: red;
    }

        .custom-row-colors .k-grid-table .k-master-row:hover {
            background-color: pink;
        }
        .custom-row-colors .k-grid-table .k-master-row.k-alt {
            background-color: green;
        }

            .custom-row-colors .k-grid-table .k-master-row.k-alt:hover {
                background-color: cyan;
            }
</style>

<TelerikGrid Data="@MyData" Height="400px" Class="custom-row-colors"
             Pageable="true" Sortable="true" Groupable="true"
             FilterMode="Telerik.Blazor.GridFilterMode.FilterRow"
             Resizable="true" Reorderable="true">
    <GridColumns>
        <GridColumn Field="@(nameof(SampleData.Id))" Width="120px" />
        <GridColumn Field="@(nameof(SampleData.Name))" Title="Employee Name" Groupable="false" />
        <GridColumn Field="@(nameof(SampleData.Team))" Title="Team" />
        <GridColumn Field="@(nameof(SampleData.HireDate))" Title="Hire Date" />
    </GridColumns>
</TelerikGrid>

@code {
    public IEnumerable<SampleData> MyData = Enumerable.Range(1, 30).Select(x => new SampleData
    {
        Id = x,
        Name = "name " + x,
        Team = "team " + x % 5,
        HireDate = DateTime.Now.AddDays(-x).Date
    });

    public class SampleData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Team { get; set; }
        public DateTime HireDate { get; set; }
    }
}

# TicketDeleteHandler Version 1 using StateHasChanged

    private async Task TicketDeleteHandler(ProfitabilityNCPDPModel DeletedEntity, ChangeEventArgs e)
    {
        System.Diagnostics.Debug.WriteLine("Ticket Selected Value= " + DeletedEntity.TicketSelected + " User Input Value =" + e.Value.ToString());

        bool isConfirmed = false;
        if (e.Value.ToString() == "True")
        {
            isConfirmed = await Dialogs.ConfirmAsync("Are you sure you want to delete the dispense record?", "Delete Confirmation");
        }
        else
        {
            isConfirmed = await Dialogs.ConfirmAsync("Are you sure you want to undelete the dispense record?", "Delete Confirmation");
        }

        if (isConfirmed == false)
        {
            if (e.Value.ToString() == "True")
            { DeletedEntity.TicketSelected = false; }

            else
            { DeletedEntity.TicketSelected = true; }

            StateHasChanged();
            return;
        }

        DeletedEntity.UpdatedBy = "" + StateHelper.Items["UserIdentityName"].ToString();
        DeletedEntity.TicketSelected = !DeletedEntity.TicketSelected;
        DeletedEntity.ticket_item_delflag_overlay = (DeletedEntity.TicketSelected == true) ? 1 : 0;
        if (DeletedEntity.TicketSelected)
        {
            DeletedEntity.CssClassName = "table-active strikeout";
        }
        else
        {
            DeletedEntity.CssClassName = "table-active";
        }

        toastService.ShowInfo(DeletedEntity.MessageDelete);
        string ChangeHistory = " Deleted ticket no #" + DeletedEntity.TICKNO + " for invoice no #" + DeletedEntity.ncpdp_invoice_number + " Please refer to ticket no ";
        int result = Task.Run(() => cprsqlService.TICKCI_UpdateDelFlag(DeletedEntity)).Result;
        DeletedEntity.Events = DeletedEntity.Events + "Delete_Cost";   //use Delete to delete ticket only and leave original cost values in there. Use Delete_Cost if you want to delete the ticket and refresh it with new cost

        await refreshRecords("RXNO");  // To do remove the page refresh
        AuditChanges = Task.Run(() => cprsqlService.GetProfitabilityAudit(DeletedEntity.ncpdp_rx_number, DeletedEntity.ncpdp_invoice_number)).Result;

        ProfitabilityNCPDPModel EntityUpdated = Entities.Where(x => x.ncpdp_invoice_number == DeletedEntity.ncpdp_invoice_number && x.TICKNO == DeletedEntity.TICKNO).FirstOrDefault();

        //ProfitabilityNCPDPModel EntityCalculate = UpdateEntity.Calculate();
        ProfitabilityNCPDPModel EntityCalculate = EntityUpdated; // Get the calculations from the database

        await cprsqlService.DataWarehouse_NCPDP_Adjustment_Update(EntityCalculate.ncpdp_rx_number, EntityCalculate.ncpdp_invoice_number, EntityCalculate.TICKNO, EntityCalculate.ncpdp_primary_claim_paid, EntityCalculate.ncpdp_primary_claim_paid_response_sys_id, EntityCalculate.ncpdp_secondary_claim_paid, EntityCalculate.ncpdp_secondary_claim_paid_response_sys_id, EntityCalculate.Copay, EntityCalculate.ncpdp_patient_copay_expected_response_sys_id, EntityCalculate.COGS, EntityCalculate.COGSAdjusted, EntityCalculate.TPRevenuePlusPatientCopay, EntityCalculate.GrossProfit, EntityCalculate.GrossProfitAdjusted, EntityCalculate.History, DeletedEntity.Events, EntityCalculate.ticket_partials_ticket_number, EntityCalculate.ticket_item_delflag_overlay);

        // For Partials find the associated ticket and put a change history in there
        if (ViewModel.SelectedSearchType == ProfitabilityReportViewModel.SearchTypes.Partials.ToString())
        {
            List<ProfitabilityNCPDPModel> EntityPartials = Entities.Where(x => x.ncpdp_invoice_number == EntityCalculate.ncpdp_invoice_number && x.TICKNO != EntityCalculate.TICKNO).ToList();

            for (int i = 0; i < EntityPartials.Count; i++)
            {
                EntityCalculate = EntityPartials[i];
                EntityCalculate.History = ChangeHistory + EntityCalculate.TICKNO + " for original values.";
                await cprsqlService.DataWarehouse_NCPDP_Adjustment_Update(EntityCalculate.ncpdp_rx_number, EntityCalculate.ncpdp_invoice_number, EntityCalculate.TICKNO, EntityCalculate.ncpdp_primary_claim_paid, EntityCalculate.ncpdp_primary_claim_paid_response_sys_id, EntityCalculate.ncpdp_secondary_claim_paid, EntityCalculate.ncpdp_secondary_claim_paid_response_sys_id, EntityCalculate.Copay, EntityCalculate.ncpdp_patient_copay_expected_response_sys_id, EntityCalculate.COGS, EntityCalculate.COGSAdjusted, EntityCalculate.TPRevenuePlusPatientCopay, EntityCalculate.GrossProfit, EntityCalculate.GrossProfitAdjusted, EntityCalculate.History, "Cost", EntityCalculate.ticket_partials_ticket_number, EntityCalculate.ticket_item_delflag_overlay);
            }
        }
    }

#Status Panel
<TelerikWindow Class="demo-window" Width="600px" Height="350px" @bind-Top="@Top" @bind-Left="@Left" Visible="true">
    <WindowTitle>
        <strong>Welcome to Profitability Report Editor!</strong>
    </WindowTitle>
    <WindowActions>
        <WindowAction Name="Minimize"></WindowAction>
        <WindowAction Name="Close"></WindowAction>
    </WindowActions>
    <WindowContent>
        <form class="k-form">
            <fieldset class="k-form-fieldset">
                <legend class="k-form-legend">Welcome to Profitability Report Editor!</legend>
                <ul>
                    <li>Use this tool to edit the accounting issues that cannot be resolved within CPR+.</li>
                    <li>Due to limited resources the maximum date range search should be one month. Ideally, if you are using the column sort and filter features, the smaller data (shorter date range), the better.</li>
                    <li>Each columns can be sorted and filtered similar to Excel.</li>
                    <li>Large data extraction (several months to years worth of data) should be done from CareTend BI.</li>
                    <li>Datagrid "Adjudicated NCPDP Report with Overlay" [0041JA] data is updated as soon as changes are made from the editor. This Datagrid was created to resemble the profitability report.</li>
                    <li>Datagrid "NCPDP_for_DRL with overlay" [0046JA] is the master template datagrid for the MAHA/DRL Excel file for Alphascript. Just like 0041JA, this Datagrid will contain all the edits made from the editor.</li>
                </ul>
            </fieldset>
        </form>
    </WindowContent>
</TelerikWindow>

#Using Count expressions

        <TelerikGrid Data="@Entities" TItem="ProfitabilityNCPDPModel" EditMode="@GridEditMode.Inline" @ref="@GridRef" Height=@(Entities != null && Entities.Count() > 1 ? "400px" : "900px") RowHeight="60" PageSize=@ViewModel.PageSize
                     ScrollMode="@(Entities != null && Entities.Count() > 60 ? GridScrollMode.Virtual : GridScrollMode.Scrollable)"
                     Sortable="true" Reorderable="true" FilterMode="Telerik.Blazor.GridFilterMode.FilterMenu" AutoGenerateColumns="false"
                     OnRowRender="@OnRowRenderHandler">

# Resources
https://stackoverflow.com/questions/13099834/how-to-get-the-display-name-attribute-of-an-enum-member-via-mvc-razor-code

