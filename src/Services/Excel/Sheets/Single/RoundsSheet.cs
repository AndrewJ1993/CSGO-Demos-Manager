﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CSGO_Demos_Manager.Models;
using CSGO_Demos_Manager.Models.Events;
using DemoInfo;
using NPOI.SS.UserModel;

namespace CSGO_Demos_Manager.Services.Excel.Sheets.Single
{
	public class RoundsSheet : AbstractSingleSheet
	{
		public RoundsSheet(IWorkbook workbook, Demo demo)
		{
			Headers = new Dictionary<string, CellType>(){
				{ "Number", CellType.Numeric },
				{ "Tick", CellType.Numeric},
				{ "Duration (s)", CellType.Numeric},
				{ "Winner Clan Name", CellType.String },
				{ "Winner", CellType.String },
				{ "End reason", CellType.String },
				{ "Type", CellType.String },
				{ "Side", CellType.String },
				{ "Team", CellType.String },
				{ "Kills", CellType.Numeric },
				{ "1K", CellType.Numeric },
				{ "2K", CellType.Numeric },
				{ "3K", CellType.Numeric },
				{ "4K", CellType.Numeric },
				{ "5K", CellType.Numeric },
				{ "Jump kills", CellType.Numeric },
				{ "ADP", CellType.Numeric },
				{ "TDH", CellType.Numeric },
				{ "TDA", CellType.Numeric },
				{ "Bomb Exploded", CellType.Numeric },
				{ "Bomb planted", CellType.Numeric },
				{ "Bomb defused", CellType.Numeric },
				{ "Start money team 1", CellType.Numeric },
				{ "Start money team 2", CellType.Numeric },
				{ "Equipement value team 1", CellType.Numeric },
				{ "Equipement value team 2", CellType.Numeric },
				{ "Flashbang", CellType.Numeric },
				{ "Smoke", CellType.Numeric },
				{ "HE", CellType.Numeric },
				{ "Decoy", CellType.Numeric },
				{ "Molotov", CellType.Numeric },
				{ "Incendiary", CellType.Numeric }
			};
			Demo = demo;
			Sheet = workbook.CreateSheet("Rounds");
		}

		public override async Task GenerateContent()
		{
			await Task.Factory.StartNew(() =>
			{
				var rowNumber = 1;

				foreach (Round round in Demo.Rounds)
				{
					round.FlashbangThrownCount = 0;
					round.SmokeThrownCount = 0;
					round.HeGrenadeThrownCount = 0;
					round.DecoyThrownCount = 0;
					round.MolotovThrownCount = 0;
					round.IncendiaryThrownCount = 0;
					foreach (WeaponFire weaponFire in Demo.WeaponFired)
					{
						if (Properties.Settings.Default.SelectedPlayerSteamId != 0
						& weaponFire.ShooterSteamId != Properties.Settings.Default.SelectedPlayerSteamId
						|| Properties.Settings.Default.SelectedStatsAccountSteamID != 0
						& weaponFire.ShooterSteamId != Properties.Settings.Default.SelectedStatsAccountSteamID) continue;
						if (weaponFire.RoundNumber == round.Number)
						{
							switch (weaponFire.Weapon.Element)
							{
								case EquipmentElement.Flash:
									round.FlashbangThrownCount++;
									break;
								case EquipmentElement.Smoke:
									round.SmokeThrownCount++;
									break;
								case EquipmentElement.Decoy:
									round.DecoyThrownCount++;
									break;
								case EquipmentElement.Molotov:
									round.MolotovThrownCount++;
									break;
								case EquipmentElement.Incendiary:
									round.IncendiaryThrownCount++;
									break;
								case EquipmentElement.HE:
									round.HeGrenadeThrownCount++;
									break;
							}
						}
					}

					IRow row = Sheet.CreateRow(rowNumber);
					int columnNumber = 0;
					SetCellValue(row, columnNumber++, CellType.Numeric, round.Number);
					SetCellValue(row, columnNumber++, CellType.Numeric, round.Tick);
					SetCellValue(row, columnNumber++, CellType.Numeric, round.Duration);
					SetCellValue(row, columnNumber++, CellType.String, round.WinnerName);
					SetCellValue(row, columnNumber++, CellType.String, round.WinnerSideAsString);
					SetCellValue(row, columnNumber++, CellType.String, round.EndReasonAsString);
					SetCellValue(row, columnNumber++, CellType.String, round.RoundTypeAsString);
					SetCellValue(row, columnNumber++, CellType.String, round.SideTroubleAsString);
					SetCellValue(row, columnNumber++, CellType.String, round.TeamTroubleName != string.Empty ? round.TeamTroubleName : string.Empty);
					SetCellValue(row, columnNumber++, CellType.Numeric, round.KillCount);
					SetCellValue(row, columnNumber++, CellType.Numeric, round.OneKillCount);
					SetCellValue(row, columnNumber++, CellType.Numeric, round.TwoKillCount);
					SetCellValue(row, columnNumber++, CellType.Numeric, round.ThreeKillCount);
					SetCellValue(row, columnNumber++, CellType.Numeric, round.FourKillCount);
					SetCellValue(row, columnNumber++, CellType.Numeric, round.FiveKillCount);
					SetCellValue(row, columnNumber++, CellType.Numeric, round.JumpKillCount);
					SetCellValue(row, columnNumber++, CellType.Numeric, round.AverageDamage);
					SetCellValue(row, columnNumber++, CellType.Numeric, round.DamageHealthCount);
					SetCellValue(row, columnNumber++, CellType.Numeric, round.DamageArmorCount);
					SetCellValue(row, columnNumber++, CellType.Numeric, round.BombExplodedCount);
					SetCellValue(row, columnNumber++, CellType.Numeric, round.BombPlantedCount);
					SetCellValue(row, columnNumber++, CellType.Numeric, round.BombDefusedCount);
					SetCellValue(row, columnNumber++, CellType.Numeric, round.StartMoneyTeam1);
					SetCellValue(row, columnNumber++, CellType.Numeric, round.StartMoneyTeam2);
					SetCellValue(row, columnNumber++, CellType.Numeric, round.EquipementValueTeam1);
					SetCellValue(row, columnNumber++, CellType.Numeric, round.EquipementValueTeam2);
					SetCellValue(row, columnNumber++, CellType.Numeric, round.FlashbangThrownCount);
					SetCellValue(row, columnNumber++, CellType.Numeric, round.SmokeThrownCount);
					SetCellValue(row, columnNumber++, CellType.Numeric, round.HeGrenadeThrownCount);
					SetCellValue(row, columnNumber++, CellType.Numeric, round.DecoyThrownCount);
					SetCellValue(row, columnNumber++, CellType.Numeric, round.MolotovThrownCount);
					SetCellValue(row, columnNumber, CellType.Numeric, round.IncendiaryThrownCount);

					rowNumber++;
				}
			});
		}
	}
}