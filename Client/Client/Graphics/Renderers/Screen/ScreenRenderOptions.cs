// This file is part of Mystery Dungeon eXtended.

// Copyright (C) 2015 Pikablu, MDX Contributors, PMU Staff

// Mystery Dungeon eXtended is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.

// Mystery Dungeon eXtended is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.

// You should have received a copy of the GNU General Public License
// along with Mystery Dungeon eXtended.  If not, see <http://www.gnu.org/licenses/>.


using System;
using System.Collections.Generic;
using System.Text;
using Client.Logic.Maps;
using SdlDotNet.Graphics;

namespace Client.Logic.Graphics.Renderers.Screen
{
    class ScreenRenderOptions
    {
        public Map Map { get; set; }
        public bool DisplayAnimation { get; set; }
        public bool DisplayAttributes { get; set; }
        public bool DisplayDungeonValues { get; set; }
        public bool DisplayMapGrid { get; set; }
        public bool DisplayLocation { get; set; }
        public int RecentPing { get; set; }
        public int RecentFPS { get; set; }
        public int RecentRenders { get; set; }
        public bool ScreenVisible { get; set; }
        public bool PlayersVisible { get; set; }
        public bool NpcsVisible { get; set; }
        public bool MinimapVisible { get; set; }
        public Graphics.Effects.Overlays.IOverlay Overlay { get; set; }
        public Graphics.Effects.Overlays.DarknessOverlay Darkness { get; set; }
        public Graphics.Effects.Weather.IWeather Weather { get; set; }
        public Graphics.Effects.Overlays.IOverlay ScreenOverlay { get; set; }

        List<Stories.Components.ScreenImageOverlay> screenImageOverlays;
        public Surface StoryBackground { get; set; }

        public List<Stories.Components.ScreenImageOverlay> ScreenImageOverlays {
            get { return screenImageOverlays; }
        }

        public ScreenRenderOptions() {
            ScreenVisible = true;
            screenImageOverlays = new List<Stories.Components.ScreenImageOverlay>();

            MinimapVisible = true;
        }

        public void SetWeather(Enums.Weather weather) {
            //weather = Enums.Weather.Raining;
            if (Weather != null) {
                if (Weather.ID != weather) {
                    Weather.FreeResources();
                } else {
                    // Same weather as the active one, don't do anything
                    return;
                }
            }
            switch (weather) {
            	case Enums.Weather.Ambiguous:
                case Enums.Weather.None: {
                        Weather = null;
                    }
                    break;
                case Enums.Weather.Raining: {
                        Weather = new Graphics.Effects.Weather.Rain();
                    }
                    break;
                case Enums.Weather.Snowing: {
                        Weather = new Graphics.Effects.Weather.Snow();
                    }
                    break;
                case Enums.Weather.Thunder: {
                    	Weather = new Graphics.Effects.Weather.Thunder();
                    }
                    break;
                case Enums.Weather.Hail: {
                        Weather = new Graphics.Effects.Weather.Hail();
                    }
                    break;
                case Enums.Weather.DiamondDust: {
                        Weather = new Graphics.Effects.Weather.DiamondDust();
                    }
                    break;
                case Enums.Weather.Cloudy: {
                        Weather = new Graphics.Effects.Weather.Cloudy();
                    }
                    break;
                case Enums.Weather.Fog: {
                        Weather = new Graphics.Effects.Weather.Fog();
                    }
                    break;
                case Enums.Weather.Sunny: {
                        Weather = new Graphics.Effects.Weather.Sunny();
                    }
                    break;
                case Enums.Weather.Sandstorm: {
                        Weather = new Graphics.Effects.Weather.Sandstorm();
                    }
                    break;
                case Enums.Weather.Snowstorm: {
                        Weather = new Graphics.Effects.Weather.Snowstorm();
                    }
                    break;
                case Enums.Weather.Ashfall: {
                        Weather = new Graphics.Effects.Weather.Ashfall();
                    }
                    break;
                    default: {
                    	
                    }
                    break;
            }
        }

        public void SetOverlay(Enums.Overlay overlay) {
            //overlay = Enums.Overlay.None;
            if (Overlay != null) {
                Overlay.FreeResources();
            }
            switch (overlay) {
                case Enums.Overlay.Night: {
                        Overlay = new Graphics.Effects.Overlays.NightOverlay();
                    }
                    break;
                case Enums.Overlay.Dawn:
                    {
                        Overlay = new Graphics.Effects.Overlays.DawnOverlay();
                    }
                    break;
                case Enums.Overlay.Dusk:
                    {
                        Overlay = new Graphics.Effects.Overlays.DuskOverlay();
                    }
                    break;
                default:
                    if (Logic.Maps.MapHelper.ActiveMap.Indoors == false) {
                        switch (Globals.GameTime)
                        {
                            case (Enums.Time.Dawn): {
                                    SetOverlay(Enums.Overlay.Dawn);
                                }
                                break;
                            case (Enums.Time.Dusk):
                                {
                                    SetOverlay(Enums.Overlay.Dusk);
                                }
                                break;
                            case (Enums.Time.Night):
                                {
                                    SetOverlay(Enums.Overlay.Night);
                                }
                                break;
                            default:
                                {
                                    Overlay = null;
                                }
                                break;

                        }
                        
                    } else {
                        Overlay = null;
                    }
                    break;
            }
        }

        public void SetDarkness(int range) {
            if (range > -1) {
                Darkness = new Graphics.Effects.Overlays.DarknessOverlay(range);
            } else {
                Darkness = null;
            }
        }

    }
}
