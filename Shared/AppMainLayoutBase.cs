using Microsoft.AspNetCore.Components;
using MudBlazor;
using Microsoft.JSInterop;
using ntgroup.Services;

namespace ntgroup.Shared;
public class AppMainLayoutBase : LayoutComponentBase
{
    protected MudTheme _theme = new MudTheme()
    {
        // Thay đổi font mặt định của MudBlazor
        Typography = new Typography()
        {
            Default = new DefaultTypography()
            {
                FontFamily = new[] { "Trirong", "sans-serif" },
            }
        },
        //  // Thay đổi trạng thái dark và light
        // PaletteLight = new PaletteLight()
        // {
        //     // Đổi màu thanh appbar
        //     AppbarBackground = Colors.Gray.Lighten2,
        // },
        // PaletteDark = new PaletteDark()
        // {
        //     // Đổi màu thanh appbar
        //     AppbarBackground = Colors.Gray.Darken3,
        // },
    };

    // Drawer navigation
    protected bool _drawerOpen = false;
    // Anchor show navigation position
    protected Anchor anchor;
    // Navigation size
    protected string? width, height;

    // Trược cảm ứng từ trái sang phải để hiển thị navigation cho mobile và tablet
    protected void OnSwipeEnd(SwipeEventArgs e)
    {
        if (e.SwipeDirection == SwipeDirection.LeftToRight && !_drawerOpen)
        {
            _drawerOpen = true;
            StateHasChanged();
        }
        else if (e.SwipeDirection == SwipeDirection.RightToLeft && _drawerOpen)
        {
            _drawerOpen = false;
            StateHasChanged();
        }
    }

    // Hiển thị thanh navigation
    protected void DrawerToggle(Anchor _anchor)
    {
        _drawerOpen = !_drawerOpen;
        this.anchor = _anchor;
        width = "300px";
        height = "100%";
    }

    #region Nếu token fake logOut tự động clear token trên localstore
    [Inject]
    private IAuthenService authService { get; set; }
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        try
        {
            if (firstRender) await authService.GetAuthenState();
        }
        catch (Exception ex)
        {
            await authService.LogOut();
            throw new Exception("Authentication error: " + ex.Message);
        }
    }
    #endregion
}