---
document type: cmdlet
external help file: VfxSettings.dll-Help.xml
HelpUri: ''
Locale: da-DK
Module Name: VfxSettings
ms.date: 08-20-2025
PlatyPS schema version: 2024-05-01
title: Get-VfxSetting
---

# Get-VfxSetting

## SYNOPSIS

Lists out all the visual effect settings this module handles and whether or not the setting is enabled or disabled.

## SYNTAX

### __AllParameterSets

```
Get-VfxSetting [<CommonParameters>]
```

## DESCRIPTION

Lists out all the visual effect settings this module handles and whether or not the setting is enabled or disabled.

## EXAMPLES

### Example 1
```powershell
PS C:\> Get-VfxSetting | select -ExpandProperty EnabledSettings
```

Lists out all the enabled settings.

## PARAMETERS

### CommonParameters

This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable,
-InformationAction, -InformationVariable, -OutBuffer, -OutVariable, -PipelineVariable,
-ProgressAction, -Verbose, -WarningAction, and -WarningVariable. For more information, see
[about_CommonParameters](https://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

## OUTPUTS
