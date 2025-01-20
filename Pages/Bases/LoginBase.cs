using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;

namespace ntgroup.Pages.Bases;
public class LoginBase : ComponentBase
{
    protected NavigationManager NavigationManager { get; set; }
    [Inject]
    protected ISnackbar snackBar { get; set; }

    protected string token { get; set; }
    protected string resultLogin = string.Empty;
    protected string ErrorMessage { get; set; }

    //Processing
    protected bool _processing = false;

    protected override async Task OnInitializedAsync()
    {
        try
        {
          
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
    }

    //Sau khi thực hiện đăng nhập
    //--> Lấy token
    //--> Ghi token vào localstorage
    //GetAuthenticationStateAsync
    protected async Task HandleLogin_Click()
    {
        try
        {

            NavigationManager.NavigateTo("/", true);
        }
        catch (Exception ex)
        {
            snackBar.Add($"""{ex.Message}""", Severity.Error);
        }
    }
        protected async void OnValidSubmit(EditContext context)
    {
        _processing = true;
        await HandleLogin_Click();
        StateHasChanged();
    }

    protected void ResetModel_Click()
    {
    }
}