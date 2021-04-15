# UYF-SimpleInfo
A [MelonLoader](https://melonwiki.xyz/#/?id=requirements) mod for Until You Fall that puts various things like the names and or descriptions of the rewards in text files or the health in a html file for use in your streaming software like OBS.

# How to use
The files are located in:
Until You Fall\UserData\SimpleInfo\

To use the txt files simple add a Text source in OBS and in the properties of that source set it to "Read from file". Click Browse and select the txt file you want to display on your stream

Here's an example for the settings of the Text source:
![ExampleTextSourceSettings](https://i.joerkig.com/cywmp5.png)

To display the Health on your stream add a Browser source in OBS, in the properties of this source put a checkmark infront of Local file and select the Health.html
The health display is automatically centered, there are also health txt files available to use which have the Current Health and the Max Health in them. You can change the images used for health by removing the ones the come with the mod and renaming the images you want to use to the names of the original ones.

Here's an example for the settings of the Browser source:
![ExampleBrowserSourceSettings](https://i.joerkig.com/lb31jp.png)
