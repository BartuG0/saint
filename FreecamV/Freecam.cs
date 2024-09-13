using System;
using GTA;
using GTA.Math;
using GTA.Native;

namespace FreecamV
{
	// Token: 0x02000003 RID: 3
	internal class Freecam
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000021CC File Offset: 0x000003CC
		public static void Tick()
		{
			if (Freecam.FCamera == null || !Freecam.FCamera.Equals(World.RenderingCamera) || Game.IsPaused)
			{
				return;
			}
			Function.Call(-2436740031033063201L, Array.Empty<InputArgument>());
			if (!Freecam.Lock)
			{
				Game.DisableAllControlsThisFrame();
			}
			if (Freecam.HUD)
			{
				Freecam.scaleform.CallFunction("DRAW_INSTRUCTIONAL_BUTTONS", new object[]
				{
					-1
				});
				Freecam.scaleform.Render2D();
			}
			Vector3 vector = Freecam.ProcessNewPos(Freecam.FCamera.Position);
			if (!Function.Call<bool>(1549119181875577954L, Array.Empty<InputArgument>()))
			{
				Function.Call(-6851178707428514157L, new InputArgument[]
				{
					false
				});
			}
			Freecam.FCamera.Position = vector;
			Freecam.FCamera.Rotation = new Vector3(Freecam.OffsetRotX, Freecam.OffsetRotY, Freecam.OffsetRotZ);
			Function.Call(-4939229729199161819L, new InputArgument[]
			{
				vector.X,
				vector.Y,
				vector.Z,
				0f,
				0f,
				0f
			});
			if (!Freecam.Lock)
			{
				if (Freecam.Attached && Game.IsControlJustPressed(238))
				{
					Freecam.FCamera.Detach();
					Freecam.AttachedEntity = null;
					Freecam.Attached = false;
					Freecam.scaleform.CallFunction("SET_DATA_SLOT", new object[]
					{
						12,
						Function.Call<string>(331533201183454215L, new InputArgument[]
						{
							2,
							237,
							0
						}),
						"Attach"
					});
					Freecam.scaleform.CallFunction("DRAW_INSTRUCTIONAL_BUTTONS", new object[]
					{
						-1
					});
				}
				else if (Game.IsControlJustPressed(237))
				{
					Entity entityInFrontOfCam = Freecam.GetEntityInFrontOfCam(Freecam.FCamera);
					if (entityInFrontOfCam != null)
					{
						Freecam.AttachedEntity = entityInFrontOfCam;
						Freecam.OffsetCoords = Function.Call<Vector3>(2482816124249826099L, new InputArgument[]
						{
							Freecam.AttachedEntity,
							Freecam.FCamera.Position.X,
							Freecam.FCamera.Position.Y,
							Freecam.FCamera.Position.Z
						});
						Freecam.FCamera.AttachTo(Freecam.AttachedEntity, new Vector3(Freecam.OffsetCoords.X, Freecam.OffsetCoords.Y, Freecam.OffsetCoords.Z));
						Freecam.Attached = true;
						Freecam.scaleform.CallFunction("SET_DATA_SLOT", new object[]
						{
							12,
							Function.Call<string>(331533201183454215L, new InputArgument[]
							{
								2,
								238,
								0
							}),
							"Detach"
						});
						Freecam.scaleform.CallFunction("DRAW_INSTRUCTIONAL_BUTTONS", new object[]
						{
							-1
						});
					}
				}
				if (Game.IsControlJustPressed(74))
				{
					Freecam.HUD = !Freecam.HUD;
				}
				if (Game.IsControlPressed(188))
				{
					Freecam.FCamera.FieldOfView -= 1f;
				}
				else if (Game.IsControlPressed(187))
				{
					Freecam.FCamera.FieldOfView += 1f;
				}
				if (Game.IsControlJustPressed(189))
				{
					if (Freecam.FilterIndex == 0)
					{
						Freecam.FilterIndex = Config.Filters.Count - 1;
					}
					else
					{
						Freecam.FilterIndex--;
					}
					Function.Call(3211975551654944577L, new InputArgument[]
					{
						Config.Filters[Freecam.FilterIndex]
					});
					Function.Call(-9013954871696349517L, new InputArgument[]
					{
						Config.FilterIntensity
					});
					Freecam.scaleform.CallFunction("SET_DATA_SLOT", new object[]
					{
						8,
						Function.Call<string>(331533201183454215L, new InputArgument[]
						{
							2,
							189,
							0
						}),
						"Filter: [" + Config.Filters[Freecam.FilterIndex] + "]"
					});
					Freecam.scaleform.CallFunction("DRAW_INSTRUCTIONAL_BUTTONS", new object[]
					{
						-1
					});
				}
				else if (Game.IsControlJustPressed(190))
				{
					if (Freecam.FilterIndex == Config.Filters.Count - 1)
					{
						Freecam.FilterIndex = 0;
					}
					else
					{
						Freecam.FilterIndex++;
					}
					Function.Call(3211975551654944577L, new InputArgument[]
					{
						Config.Filters[Freecam.FilterIndex]
					});
					Function.Call(-9013954871696349517L, new InputArgument[]
					{
						Config.FilterIntensity
					});
					Freecam.scaleform.CallFunction("SET_DATA_SLOT", new object[]
					{
						8,
						Function.Call<string>(331533201183454215L, new InputArgument[]
						{
							2,
							189,
							0
						}),
						"Filter: [" + Config.Filters[Freecam.FilterIndex] + "]"
					});
					Freecam.scaleform.CallFunction("DRAW_INSTRUCTIONAL_BUTTONS", new object[]
					{
						-1
					});
				}
				else if (Game.IsControlJustPressed(45))
				{
					Freecam.Disable();
					Freecam.Enable();
					Freecam.FilterIndex = 0;
					Function.Call(3211975551654944577L, new InputArgument[]
					{
						"None"
					});
					Freecam.scaleform.CallFunction("SET_DATA_SLOT", new object[]
					{
						8,
						Function.Call<string>(331533201183454215L, new InputArgument[]
						{
							2,
							189,
							0
						}),
						"Filter: [" + Config.Filters[Freecam.FilterIndex] + "]"
					});
					Freecam.scaleform.CallFunction("DRAW_INSTRUCTIONAL_BUTTONS", new object[]
					{
						-1
					});
				}
				if (Game.IsControlJustPressed(47))
				{
					if (!Freecam.SlowMode)
					{
						Game.TimeScale /= Config.SlowMotionMultiplier;
					}
					else
					{
						Game.TimeScale = 1f;
					}
					Freecam.SlowMode = !Freecam.SlowMode;
				}
				if (Game.IsControlJustPressed(75))
				{
					Freecam.SlowMode = !Freecam.SlowMode;
					Freecam.Frozen = !Freecam.Frozen;
					Game.Pause(Freecam.Frozen);
				}
			}
			if (Game.IsControlJustPressed(201))
			{
				Freecam.Lock = !Freecam.Lock;
				if (Freecam.Lock)
				{
					Game.Pause(false);
					Freecam.HUD = false;
					return;
				}
				Freecam.HUD = true;
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002934 File Offset: 0x00000B34
		public static Vector3 ProcessNewPos(Vector3 CurrentPos)
		{
			Vector3 result = CurrentPos;
			if (Function.Call<bool>(-6525200845567248616L, new InputArgument[]
			{
				0
			}) && !Freecam.Lock)
			{
				if (Game.IsControlPressed(32))
				{
					float num = Function.Call<float>(841539415165780831L, new InputArgument[]
					{
						Freecam.OffsetRotZ
					});
					float num2 = Function.Call<float>(-3386793356200111204L, new InputArgument[]
					{
						Freecam.OffsetRotZ
					});
					float num3 = Function.Call<float>(841539415165780831L, new InputArgument[]
					{
						Freecam.OffsetRotX
					});
					result.X -= (float)(0.1 * (double)Freecam.Speed * (double)num);
					result.Y += (float)(0.1 * (double)Freecam.Speed * (double)num2);
					result.Z += (float)(0.1 * (double)Freecam.Speed * (double)num3);
				}
				if (Game.IsControlPressed(33))
				{
					float num4 = Function.Call<float>(841539415165780831L, new InputArgument[]
					{
						Freecam.OffsetRotZ
					});
					float num5 = Function.Call<float>(-3386793356200111204L, new InputArgument[]
					{
						Freecam.OffsetRotZ
					});
					float num6 = Function.Call<float>(841539415165780831L, new InputArgument[]
					{
						Freecam.OffsetRotX
					});
					result.X += (float)(0.1 * (double)Freecam.Speed * (double)num4);
					result.Y -= (float)(0.1 * (double)Freecam.Speed * (double)num5);
					result.Z -= (float)(0.1 * (double)Freecam.Speed * (double)num6);
				}
				if (Game.IsControlPressed(34))
				{
					float num7 = Function.Call<float>(841539415165780831L, new InputArgument[]
					{
						Freecam.OffsetRotZ + 90f
					});
					float num8 = Function.Call<float>(-3386793356200111204L, new InputArgument[]
					{
						Freecam.OffsetRotZ + 90f
					});
					result.X -= (float)(0.1 * (double)Freecam.Speed * (double)num7);
					result.Y += (float)(0.1 * (double)Freecam.Speed * (double)num8);
				}
				if (Game.IsControlPressed(35))
				{
					float num9 = Function.Call<float>(841539415165780831L, new InputArgument[]
					{
						Freecam.OffsetRotZ + 90f
					});
					float num10 = Function.Call<float>(-3386793356200111204L, new InputArgument[]
					{
						Freecam.OffsetRotZ + 90f
					});
					result.X += (float)(0.1 * (double)Freecam.Speed * (double)num9);
					result.Y -= (float)(0.1 * (double)Freecam.Speed * (double)num10);
				}
				if (Game.IsControlPressed(22))
				{
					result.Z += (float)(0.1 * (double)Freecam.Speed);
				}
				if (Game.IsControlPressed(36))
				{
					result.Z -= (float)(0.1 * (double)Freecam.Speed);
				}
				if (Game.IsControlPressed(21))
				{
					Freecam.Speed = Config.ShiftSpeed;
				}
				else
				{
					Freecam.Speed = Config.DefaultSpeed;
				}
				Freecam.OffsetRotX -= Game.GetDisabledControlValueNormalized(2) * Config.Precision * 8f;
				Freecam.OffsetRotZ -= Game.GetDisabledControlValueNormalized(1) * Config.Precision * 8f;
				if (Game.IsControlPressed(44))
				{
					Freecam.OffsetRotY -= Config.Precision;
				}
				if (Game.IsControlPressed(38))
				{
					Freecam.OffsetRotY += Config.Precision;
				}
			}
			if ((double)Freecam.OffsetRotX > 90.0)
			{
				Freecam.OffsetRotX = 90f;
			}
			else if (Freecam.OffsetRotX < -90f)
			{
				Freecam.OffsetRotX = -90f;
			}
			return result;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002D58 File Offset: 0x00000F58
		public static void Enable()
		{
			Freecam.FCamera = World.CreateCamera(GameplayCamera.Position, GameplayCamera.Rotation, GameplayCamera.FieldOfView);
			Freecam.FCamera.Direction = GameplayCamera.Direction;
			Function.Call(-6851178707428514157L, new InputArgument[]
			{
				false
			});
			Freecam.HUD = true;
			Function.Call(-9013954871696349517L, new InputArgument[]
			{
				Config.FilterIntensity
			});
			Function.Call(3211975551654944577L, new InputArgument[]
			{
				Config.Filters[Freecam.FilterIndex]
			});
			World.RenderingCamera = Freecam.FCamera;
			Freecam.Init();
			if (Freecam.SlowMode)
			{
				Game.TimeScale /= Config.SlowMotionMultiplier;
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002E24 File Offset: 0x00001024
		public static void Disable()
		{
			Freecam.FCamera.Delete();
			Game.Player.Character.IsCollisionEnabled = true;
			Function.Call(-6851178707428514157L, new InputArgument[]
			{
				true
			});
			Function.Call(3582399230505917858L, Array.Empty<InputArgument>());
			Function.Call(3211975551654944577L, new InputArgument[]
			{
				"None"
			});
			World.RenderingCamera = null;
			Game.Pause(false);
			Freecam.Frozen = false;
			Freecam.Attached = false;
			Freecam.Lock = false;
			Freecam.OffsetRotX = 0f;
			Freecam.OffsetRotY = 0f;
			Freecam.OffsetRotZ = 0f;
			if (Freecam.SlowMode)
			{
				Game.TimeScale = 1f;
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002EEC File Offset: 0x000010EC
		public static void Init()
		{
			Freecam.scaleform = new Scaleform("instructional_buttons");
			Freecam.scaleform.CallFunction("CLEAR_ALL", new object[0]);
			Freecam.scaleform.CallFunction("TOGGLE_MOUSE_BUTTONS", new object[]
			{
				0
			});
			Freecam.scaleform.CallFunction("SET_DATA_SLOT", new object[]
			{
				0,
				Function.Call<string>(331533201183454215L, new InputArgument[]
				{
					2,
					30,
					0
				}),
				""
			});
			Freecam.scaleform.CallFunction("SET_DATA_SLOT", new object[]
			{
				1,
				Function.Call<string>(331533201183454215L, new InputArgument[]
				{
					2,
					31,
					0
				}),
				"Move"
			});
			Freecam.scaleform.CallFunction("SET_DATA_SLOT", new object[]
			{
				2,
				Function.Call<string>(331533201183454215L, new InputArgument[]
				{
					2,
					1,
					0
				}),
				"Look"
			});
			Freecam.scaleform.CallFunction("SET_DATA_SLOT", new object[]
			{
				3,
				Function.Call<string>(331533201183454215L, new InputArgument[]
				{
					2,
					38,
					0
				}),
				""
			});
			Freecam.scaleform.CallFunction("SET_DATA_SLOT", new object[]
			{
				4,
				Function.Call<string>(331533201183454215L, new InputArgument[]
				{
					2,
					44,
					0
				}),
				"Roll"
			});
			Freecam.scaleform.CallFunction("SET_DATA_SLOT", new object[]
			{
				5,
				Function.Call<string>(331533201183454215L, new InputArgument[]
				{
					2,
					187,
					0
				}),
				""
			});
			Freecam.scaleform.CallFunction("SET_DATA_SLOT", new object[]
			{
				6,
				Function.Call<string>(331533201183454215L, new InputArgument[]
				{
					2,
					188,
					0
				}),
				"FOV"
			});
			Freecam.scaleform.CallFunction("SET_DATA_SLOT", new object[]
			{
				7,
				Function.Call<string>(331533201183454215L, new InputArgument[]
				{
					2,
					190,
					0
				}),
				""
			});
			Freecam.scaleform.CallFunction("SET_DATA_SLOT", new object[]
			{
				8,
				Function.Call<string>(331533201183454215L, new InputArgument[]
				{
					2,
					189,
					0
				}),
				"Filter: [" + Config.Filters[Freecam.FilterIndex] + "]"
			});
			Freecam.scaleform.CallFunction("SET_DATA_SLOT", new object[]
			{
				9,
				Function.Call<string>(331533201183454215L, new InputArgument[]
				{
					2,
					45,
					0
				}),
				"Reset"
			});
			Freecam.scaleform.CallFunction("SET_DATA_SLOT", new object[]
			{
				10,
				Function.Call<string>(331533201183454215L, new InputArgument[]
				{
					2,
					47,
					0
				}),
				"Slow Motion"
			});
			Freecam.scaleform.CallFunction("SET_DATA_SLOT", new object[]
			{
				11,
				Function.Call<string>(331533201183454215L, new InputArgument[]
				{
					2,
					75,
					0
				}),
				"Freeze"
			});
			Freecam.scaleform.CallFunction("SET_DATA_SLOT", new object[]
			{
				11,
				Function.Call<string>(331533201183454215L, new InputArgument[]
				{
					2,
					201,
					0
				}),
				"Control Lock"
			});
			if (!Freecam.Attached)
			{
				Freecam.scaleform.CallFunction("SET_DATA_SLOT", new object[]
				{
					12,
					Function.Call<string>(331533201183454215L, new InputArgument[]
					{
						2,
						237,
						0
					}),
					"Attach"
				});
			}
			else
			{
				Freecam.scaleform.CallFunction("SET_DATA_SLOT", new object[]
				{
					12,
					Function.Call<string>(331533201183454215L, new InputArgument[]
					{
						2,
						238,
						0
					}),
					"Detach"
				});
			}
			Freecam.scaleform.CallFunction("SET_DATA_SLOT", new object[]
			{
				13,
				Function.Call<string>(331533201183454215L, new InputArgument[]
				{
					2,
					74,
					0
				}),
				"Toggle HUD"
			});
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00003553 File Offset: 0x00001753
		public static void Toggle()
		{
			if (Freecam.FCamera == null || !Freecam.FCamera.Equals(World.RenderingCamera) || Game.IsPaused)
			{
				Freecam.Enable();
				return;
			}
			Freecam.Disable();
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00003588 File Offset: 0x00001788
		public static Entity GetEntityInFrontOfCam(Camera Cam)
		{
			Vector3 vector = Function.Call<Vector3>(-4989925752450652754L, new InputArgument[]
			{
				Cam
			});
			Vector3 vector2 = default(Vector3);
			vector2.X = vector.X - Function.Call<float>(841539415165780831L, new InputArgument[]
			{
				Freecam.OffsetRotZ
			}) * 100f;
			vector2.Y = vector.Y + Function.Call<float>(-3386793356200111204L, new InputArgument[]
			{
				Freecam.OffsetRotZ
			}) * 100f;
			vector2.Z = vector.Z + Function.Call<float>(841539415165780831L, new InputArgument[]
			{
				Freecam.OffsetRotX
			}) * 100f;
			Vector3 vector3 = vector2;
			RaycastResult raycastResult = World.Raycast(vector, vector3, -1, null);
			if (raycastResult.DidHit)
			{
				return raycastResult.HitEntity;
			}
			return null;
		}

		// Token: 0x04000007 RID: 7
		private static Camera FCamera;

		// Token: 0x04000008 RID: 8
		private static Entity AttachedEntity;

		// Token: 0x04000009 RID: 9
		private static Vector3 OffsetCoords = Vector3.Zero;

		// Token: 0x0400000A RID: 10
		private static Scaleform scaleform;

		// Token: 0x0400000B RID: 11
		private static bool SlowMode = true;

		// Token: 0x0400000C RID: 12
		private static bool Frozen = false;

		// Token: 0x0400000D RID: 13
		private static bool Lock = false;

		// Token: 0x0400000E RID: 14
		private static bool HUD = true;

		// Token: 0x0400000F RID: 15
		private static bool Attached = false;

		// Token: 0x04000010 RID: 16
		private static float OffsetRotX = 0f;

		// Token: 0x04000011 RID: 17
		private static float OffsetRotY = 0f;

		// Token: 0x04000012 RID: 18
		private static float OffsetRotZ = 0f;

		// Token: 0x04000013 RID: 19
		private static float Speed = 5f;

		// Token: 0x04000014 RID: 20
		private static int FilterIndex = 0;
	}
}
