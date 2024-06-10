﻿using System.Windows;

using Grapher;

public static class MainWindowNavigation
{
	private static MainWindow? _mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();

	public static void ChangePage(Uri pageUri) => _mainWindow?.GrapherNavigationFrame.Navigate(pageUri);
}