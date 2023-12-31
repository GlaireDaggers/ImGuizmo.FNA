# ImGuizmo.FNA
A demo project showcasing ImGuizmo integration with FNA

# Dependencies
ImGuizmo.FNA depends on [FNA](https://github.com/FNA-XNA/FNA) & [ImGui.NET](https://github.com/ImGuiNET/ImGui.NET/)

For ImGui.NET, ImGuizmo.FNA uses a custom fork [here](https://github.com/GlaireDaggers/ImGui.NET) due to some API differences in the version of cimguizmo used.
The native libs were compiled with a custom fork of [ImGui.NET-nativebuild](https://github.com/ImGuiNET/ImGui.NET-nativebuild). The fork can be found [here](https://github.com/GlaireDaggers/ImGui.NET-nativebuild) and adds extra libs (cimguizmo, cimplot, & cimnodes)

Make sure you `git submodule update --init --recursive` before building!

# Screenshot

![Screenshot of a rotated cube with the rotate gizmo displayed. An IMGUI window in the corner contains a "Local Space" checkbox that is ticked.](/screenshot/scr1.png?raw=true)