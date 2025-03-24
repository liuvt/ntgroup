using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace ntgroup.Shared.Owners;
public class OwnersMainLayoutBase : LayoutComponentBase
{
    protected bool _drawerOpenOwner = true;
    protected void DrawerToggle()
    {
        _drawerOpenOwner = !_drawerOpenOwner;
    }
}