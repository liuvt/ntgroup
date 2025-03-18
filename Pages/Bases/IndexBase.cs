using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using ntgroup.Data.Models;
using ntgroup.Services.Interfaces;
using MudBlazor;
using Google.Apis.Sheets.v4;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using DocumentFormat.OpenXml.Office2013.Excel;
using DocumentFormat.OpenXml.Office2010.Excel;
using ntgroup.Extensions;
using DocumentFormat.OpenXml.Office.CustomUI;
using ntgroup.Services;
using ntgroup.Data.Entities;

namespace ntgroup.Pages.Bases;
public class IndexBase : ComponentBase
{

    [Inject]
    protected ISpreadsRecuitmentService recuitmentService { get; set; }
    protected List<Job> jobs = new List<Job>();
    protected Job job = new Job();

    protected bool isLoaded = false;
    protected string countVehicle;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            jobs = await recuitmentService.GetsJobs();
            isLoaded = true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    
}   