using Swed32;
using ImGuiNET;
using ClickableTransparentOverlay;
using System.ComponentModel;

namespace mw3mod
{
    public class Program : Overlay
    {
        bool infAmmo;
        bool NoFall;
        bool maxScore;
        int ammo2;
        int score2;

        protected override void Render()
        {
            ImGui.Begin("MW3 Mod Menu");

            ImGui.Checkbox("Infinite Ammo", ref infAmmo);
            ImGui.SameLine();
            ImGui.PushItemWidth(100);
            ImGui.InputInt("Ammo", ref ammo2);
            if (infAmmo == true)
            {
                infiniteAmmo();
            }

            ImGui.Checkbox("Max Score", ref maxScore);
            ImGui.SameLine();
            ImGui.PushItemWidth(100);
            ImGui.InputInt("Score", ref score2);
            if (maxScore == true)
            {
                maximumScore();
            }

            ImGui.Checkbox("No Fall", ref NoFall);
            if (NoFall == true)
            {
                noFall();
            }

            ImGui.End();
        }

        public void infiniteAmmo()
        {
            Swed swed = new Swed("iw5mp");
            IntPtr moduleBase = swed.GetModuleBase("iw5mp.exe");
            IntPtr ammoAddress = swed.ReadPointer(moduleBase, 0x00193F4C) + 0xB0;
            int ammo = swed.ReadInt(ammoAddress);
            swed.WriteInt(ammoAddress, ammo2);
        }

        public void noFall()
        {
            Swed swed = new Swed("iw5mp");
            IntPtr moduleBase = swed.GetModuleBase("iw5mp.exe");
            IntPtr fallAddress = swed.ReadPointer(moduleBase, 0x05CD2AC8, 0xEC, 0x34, 0x128) + 0xE9C;
            int fall = swed.ReadInt(fallAddress);
            swed.WriteInt(fallAddress, 100);
        }

        public void maximumScore()
        {
            Swed swed = new Swed("iw5mp");
            IntPtr moduleBase = swed.GetModuleBase("iw5mp.exe");
            IntPtr scoreAddress = swed.ReadPointer(moduleBase, 0x011818D8, 0x294, 0x57C, 0x32C) + 0x178;
            int score = swed.ReadInt(scoreAddress);
            swed.WriteInt(scoreAddress, 9999);
        }

        public static void Main(string[] args)
        {
            // console text and program execution and hiding console
            Console.WriteLine("Starting GUI...");
            Program program = new Program();
            program.Start().Wait();
        }
    }
}