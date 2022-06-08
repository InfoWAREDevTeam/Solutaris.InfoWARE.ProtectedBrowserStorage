# Solutaris.InfoWARE.ProtectedBrowserStorage
Solutaris.InfoWARE.ProtectedBrowserStorage is a library that allows saving to browser's **Local Storage** and **Session Storage** in encrypted format for both Blazor WASM and Blazor Server Applications.

Yes! Protected Browser Storage now possible with Solutaris.InfoWARE.ProtectedBrowserStorage (it encrypts the data using AES encryption and then compresses it before saving to browser's local or session Storage).

**With Solutaris.InfoWARE.ProtectedBrowserStorage you have a protected browser storage even in Blazor WASM - which is not possible with Microsoft's Protected Browser Storage (Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage namespace) as it relies on ASP .NET Core Data Protection (only supported for Blazor Server apps)!**
		
Microsoft's Protected Browser Storage (Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage namespace) relies on ASP .NET Core Data Protection and is only supported for Blazor Server apps [More Info](https://docs.microsoft.com/en-us/aspnet/core/blazor/state-management?view=aspnetcore-6.0&pivots=webassembly).

With **Solutaris.InfoWARE.ProtectedBrowserStorage** you now have a protected browser storage in Blazor WASM!

## Installing

To install the package add the following line to you csproj file replacing x.x.x with the appropriate version number:

```
<PackageReference Include="Solutaris.InfoWARE.ProtectedBrowserStorage" Version="x.x.x" />
```

You can also install via the .NET CLI with the following command:

```
dotnet add package Solutaris.InfoWARE.ProtectedBrowserStorage
```

If you're using Visual Studio you can also install via the built in NuGet package manager.


## Add Reference to _host.cshtml or index.html file
Add this to your to index.html (For Blazor WASM), or _Layout.cshtml / _Host.cshtml (for Blazor Server)
```html
<script src="_content/Solutaris.InfoWARE.ProtectedBrowserStorage/config.js"></script>
```

## Setup in Program.cs (Blazor WASM)
```csharp
using Solutaris.InfoWARE.ProtectedBrowserStorage.Extensions;
var builder = WebAssemblyHostBuilder.CreateDefault(args);
.
.
.
builder.Services.AddIWProtectedBrowserStorageAsSingleton(); //optionally pass an encryption key (as string)

await builder.Build().RunAsync();
```


## Setup in Program.cs (Blazor Server)
```csharp
using Solutaris.InfoWARE.ProtectedBrowserStorage.Extensions;
var builder = WebApplication.CreateBuilder(args);
.
.
.
builder.Services.AddIWProtectedBrowserStorage(); //optionally pass an encryption key (as string)

await builder.Build().RunAsync();
```

## Usage (Blazor WebAssembly)
To use Solutaris.InfoWARE.ProtectedBrowserStorage's LocalStorage  in Blazor WebAssembly, inject the IIWLocalStorageService per the example below.

**NOTE:** For SessionStorage use Solutaris.InfoWARE.ProtectedBrowserStorage.Services.IIWSessionStorageService

```csharp
@inject Solutaris.InfoWARE.ProtectedBrowserStorage.Services.IIWLocalStorageService localStorage

@code {

    protected override async Task OnInitializedAsync()
    {
        await localStorage.SetItem("name", "John Doe");
        var name = await localStorage.GetItem<string>("name");
    }

}
```

## Usage (Blazor Server)
**NOTE:** 
- Due to pre-rendering in Blazor Server you can't perform any JS interop until the OnAfterRender lifecycle method.
- For SessionStorage use: **Solutaris.InfoWARE.ProtectedBrowserStorage.Services.IIWSessionStorageService**

```csharp
@inject Solutaris.InfoWARE.ProtectedBrowserStorage.Services.IIWLocalStorageService localStorage

@code {

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await localStorage.SetItemAsync("name", "John Doe");
        var name = await localStorage.GetItemAsync<string>("name");
    }

}
```

## The APIs available are:

- SetItem()
- GetItem()
- RemoveItem()
- RemoveAllItems()
- SetItemAsync()
- GetItemAsync()
- RemoveItemAsync()
- RemoveAllItemsAsync()

**Note:** 
- API Synchronous methods are strictly for Blazor WASM only, as JavaScript calls (Interoperablity) are directly available in WASM but not in Blazor Server.  
- API Asynchronous methods are supported by both Blazor Server and Blazor WASM, but not recommended for WASM - synchronous variant is best for WASM.