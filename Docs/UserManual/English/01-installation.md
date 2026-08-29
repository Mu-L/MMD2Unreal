# 01 Download & Install

This chapter covers three things: downloading the plugin, placing it into your project, and confirming it works. It takes about five minutes.

> **Note:** This manual was translated by AI from the Chinese original, which is the authoritative version. Some expressions may not be fully accurate.

## What You Need

- Unreal Engine 5.8 already installed
- An Unreal project (a blank one is fine)
- Network access to GitHub

## Step 1: Download the Plugin

1. Open the plugin's GitHub project page.
2. Find the **Releases** entry on the right side of the page and click it.
3. Versions are listed by date. The topmost entry marked **Latest** is the newest release. Just use it; there is no need to worry about specific version numbers.
4. In that release's page, find the attachments area (Assets) and download the archive file (usually ending in `.zip`).
5. When the download finishes, remember where you saved it.

## Step 2: Place the Plugin Into Your Project

1. Locate your Unreal project folder on your computer. How to recognize it: it contains a file with the same name as your project, ending in `.uproject`.
2. Check whether the folder contains a folder named **Plugins**. If not, create one yourself, spelling the name exactly.
3. Extract the archive from Step 1 and put the extracted plugin folder into Plugins.
4. Verify the result: inside the plugin folder you should directly find a file called `MMD2Unreal.uplugin`, meaning the full path looks like `YourProject/Plugins/MMD2Unreal/MMD2Unreal.uplugin`. If opening the folder shows another identically named folder inside, move the inner one up.

## Step 3: Install the Physics Component (Required)

The hair swaying and skirt fluttering effects after import depend on another free plugin called **KawaiiPhysics**. The main plugin does not include it, so install it once separately:

1. Open the [KawaiiPhysics GitHub project page](https://github.com/pafuhana1213/KawaiiPhysics).
2. Likewise go to its Releases page and download the latest archive.
3. Put it into the project's Plugins folder exactly as in Step 2.

Afterwards, your Plugins folder should contain two folders: `MMD2Unreal` and `KawaiiPhysics`.

## Step 4: Open the Project and Enable the Plugins (Usually Enabled by Default)

1. Double-click the `.uproject` file to open the project as usual.
2. In the top menu click **Edit**, then **Plugins**, and a list window appears.
3. Type `MMD2Unreal` in the search box and make sure its checkbox is ticked (it is usually already enabled on first launch; if it is enabled, skip this step).
4. Search for `Kawaii` and enable KawaiiPhysics the same way.
5. If either plugin was disabled, enable it and **restart the Unreal Editor once** so the plugins take effect.

## Step 5: Confirm the Installation Worked

The simplest test:

1. Prepare any `.pmx` model file.
2. Drag it straight into the **Content Browser** at the bottom of Unreal (the area showing asset thumbnails).
3. If an import settings window pops up, everything is ready. Follow the [next chapter](02-import-model.md) to import properly.

## Common Problems

### If the Project Asks to Rebuild or Compile

The first time the plugin loads, a window may ask whether to rebuild or compile. Check that the Unreal Engine and plugin versions match. This manual is written for Unreal Engine 5.8, and future versions may differ, so pay attention to the version numbers.

## Interface Language

The import window supports the three languages most common in the MMD community: Simplified Chinese, English, and Japanese. It automatically follows the Unreal Editor's display language; no setup inside the plugin is needed:

- Switch the editor language under **Edit > Editor Preferences > Region & Language** and the import window follows. An already-open import window must be closed and the file dragged in again to pick up the new language.
- Users on other locales automatically fall back to English.

## Next Step

The plugin is installed. Now bring in your first model: [02 Importing a Model](02-import-model.md).
