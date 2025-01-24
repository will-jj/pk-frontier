using Avalonia;
using Avalonia.Controls;
using PkFactory.ViewModels;

namespace PkFactory.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();
        AttachedToVisualTree += OnAttachedToVisualTree;
    }

    private async void OnAttachedToVisualTree(object sender, VisualTreeAttachmentEventArgs e)
    {
        if (DataContext is MainViewModel viewModel) await viewModel.GetPreppedFileCommand.ExecuteAsync(null);
    }
}