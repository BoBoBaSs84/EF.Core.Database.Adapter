function Fix-FlagsEnums {
	param(
		[string]$ContractsDirectory = "..\Contracts"
	)
	
	# Liste der Enums, die als Flags behandelt werden sollen
	# Format: "RelativerPfad:EnumName" (ohne .cs Endung)
	$FlagsEnums = @(
		# Weitere Enums hier hinzufügen:
		"Contracts:WorkDayTypes",
		"Contracts:DocumentTypes"
	)
	
	Write-Host "Fixing Flags enums in generated contracts..." -ForegroundColor Yellow
	
	# Prozessiere nur die spezifisch konfigurierten Enums
	foreach ($flagsEnum in $FlagsEnums) {
		$parts = $flagsEnum.Split(":")
		if ($parts.Length -eq 2) {
			$relativePath = $parts[0]
			$enumName = $parts[1]
			
			# Konstruiere den vollständigen Dateipfad
			$filePath = Join-Path $ContractsDirectory ($relativePath + ".cs")
			
			if (Test-Path $filePath) {
				Write-Host "Processing enum '$enumName' in file: $filePath" -ForegroundColor Cyan
				
				$content = Get-Content -Path $filePath -Raw
				$originalContent = $content
				
				# Muster für die spezifische Enum (berücksichtigt bereits vorhandene [System.Flags])
				$enumPattern = "(?s)(\[System\.CodeDom\.Compiler\.GeneratedCode[^\]]+\]\s*)(\[System\.Flags\]\s*)?(public enum $enumName\s*\{[^}]+\})"
				
				$match = [regex]::Match($content, $enumPattern)
				
				if ($match.Success) {
					$beforeEnum = $match.Groups[1].Value
					$existingFlags = $match.Groups[2].Value
					$enumDeclaration = $match.Groups[3].Value
					
					Write-Host "  Found enum '$enumName'" -ForegroundColor Green
					
					$needsUpdate = $false
					$newContent = $content
					
					# Füge [System.Flags] hinzu wenn es fehlt
					if ([string]::IsNullOrWhiteSpace($existingFlags)) {
						Write-Host "    Adding [System.Flags] attribute" -ForegroundColor Yellow
						$newEnumSection = $beforeEnum + "[System.Flags]`r`n" + "`t" + $enumDeclaration
						$newContent = $newContent.Replace($beforeEnum + $enumDeclaration, $newEnumSection)
						$needsUpdate = $true
					}
					
					# Korrigiere die Enum-Werte für Flags
					$fixedEnumDeclaration = $enumDeclaration
					
					# Finde alle Enum-Member mit numerischen Werten
					$memberPattern = '(\w+)\s*=\s*(\d+)'
					$memberMatches = [regex]::Matches($enumDeclaration, $memberPattern)
					
					foreach ($memberMatch in $memberMatches) {
						$memberName = $memberMatch.Groups[1].Value
						$memberValue = [int]$memberMatch.Groups[2].Value
						
						# Berechne den korrekten Bit-Shift Wert basierend auf der Position
						$newValue = $null
						
						if ($memberValue -eq 0) {
							# NONE bleibt 0
							continue
						}
						else {
							$newValue = "1 << " + ($memberValue - 1)
						}
						
						if ($newValue) {
							$oldPattern = "$memberName\s*=\s*$memberValue"
							$newPattern = "$memberName = $newValue"
							
							if ($fixedEnumDeclaration -match $oldPattern) {
								Write-Host "      Fixing $memberName from $memberValue to $newValue" -ForegroundColor Yellow
								$fixedEnumDeclaration = $fixedEnumDeclaration -replace $oldPattern, $newPattern
								$needsUpdate = $true
							}
						}
					}
					
					# Ersetze die ursprüngliche Enum-Deklaration mit der korrigierten
					if ($fixedEnumDeclaration -ne $enumDeclaration) {
						$newContent = $newContent.Replace($enumDeclaration, $fixedEnumDeclaration)
					}
					
					# Schreibe zurück in die Datei wenn sich etwas geändert hat
					if ($needsUpdate -and $newContent -ne $originalContent) {
						Set-Content -Path $filePath -Value $newContent -NoNewline
						Write-Host "    Updated: $([System.IO.Path]::GetFileName($filePath))" -ForegroundColor Green
					} else {
						Write-Host "    No changes needed" -ForegroundColor Gray
					}
				} else {
					Write-Host "    Enum '$enumName' not found in file" -ForegroundColor Red
				}
			} else {
				Write-Host "File not found: $filePath" -ForegroundColor Red
			}
		}
	}
	
	Write-Host "Flags enum fixing completed." -ForegroundColor Yellow
}

dotnet tool update --global --all

refitter --settings-file .\HomeApiClient.refitter

Fix-FlagsEnums -ContractsDirectory "Contracts"