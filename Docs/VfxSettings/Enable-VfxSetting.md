---
document type: cmdlet
external help file: VfxSettings.dll-Help.xml
HelpUri: ''
Locale: da-DK
Module Name: VfxSettings
ms.date: 08-20-2025
PlatyPS schema version: 2024-05-01
title: Enable-VfxSetting
---

# Enable-VfxSetting

## SYNOPSIS

Enables specified visual effects.

## SYNTAX

### __AllParameterSets

```
Enable-VfxSetting [-Setting] <VfxSetting[]> [<CommonParameters>]
```

## DESCRIPTION

Enables specified visual effects.
Most of the settings match settings found SystemPropertiesAdvanced.exe -> Performance -> Visual effects.

## EXAMPLES

### Example 1
```powershell
PS C:\> Enable-VfxSetting MouseShadow
```

Enables the mouse cursor shadow effect.

### Example 2
```powershell
PS C:\> Enable-VfxSetting (Get-VfxSetting).DisabledSettings
```

Enables every setting that is currently disabled.

## PARAMETERS

### -Setting

The following settings map to the settings found in SystemPropertiesAdvanced.exe -> Performance -> Visual effects.

| Name in module                 | Name in GUI |
| ------------------------------ | ----------- |
| AeroPeek                       | Enable Peek |
| AnimateInsideWindows           | Animate controls and elements inside windows |
| AnimateMenus                   | Fade or slide menus into view |
| AnimateMinimizeAndMaximize     | Animate windows when minimizing and maximizing |
| AnimateTaskbar                 | Animations in the taskbar |
| AnimateTooltips                | Fade or slide ToolTips into view |
| FontSmoothing                  | Smooth edges of screen fonts |
| IconShadows                    | Use drop shadows for icon labels on the desktop |
| MenuSelectionFade              | Fade out menu items after clicking |
| MouseShadow                    | Show shadows under mouse pointer |
| SaveTaskbarThumbnails          | Save taskbar thumbnail previews |
| ShowWindowContentWhileDragging | Show window contents while dragging |
| SlideOpenComboBoxes            | Slide open combo boxes |
| SmoothScrollListBoxes          | Smooth-scroll list boxes |
| Thumbnails                     | Show thumbnails instead of icons |
| TranslucentSelection           | Show translucent selection rectangle |
| Transparency                   | Transparency effects (In Windows Settings) |
| WindowShadow                   | Show shadows under windows |

```yaml
Type: MartinGC94.VfxSettings.API.VfxSetting[]
DefaultValue: ''
SupportsWildcards: false
Aliases: []
ParameterSets:
- Name: (All)
  Position: 0
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### CommonParameters

This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable,
-InformationAction, -InformationVariable, -OutBuffer, -OutVariable, -PipelineVariable,
-ProgressAction, -Verbose, -WarningAction, and -WarningVariable. For more information, see
[about_CommonParameters](https://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

## OUTPUTS