<div id="top">

<!-- HEADER STYLE: CLASSIC -->
<div align="center">

<img src="readmeai/assets/logos/purple.svg" width="30%" style="position: relative; top: 0; right: 0;" alt="Project Logo"/>

# ARDRUM-VIRTUALHAND

<em></em>

<!-- BADGES -->
<img src="https://img.shields.io/github/license/wayne0419/ARDrum-VirtualHand?style=flat-square&logo=opensourceinitiative&logoColor=white&color=FF4B4B" alt="license">
<img src="https://img.shields.io/github/last-commit/wayne0419/ARDrum-VirtualHand?style=flat-square&logo=git&logoColor=white&color=FF4B4B" alt="last-commit">
<img src="https://img.shields.io/github/languages/top/wayne0419/ARDrum-VirtualHand?style=flat-square&color=FF4B4B" alt="repo-top-language">
<img src="https://img.shields.io/github/languages/count/wayne0419/ARDrum-VirtualHand?style=flat-square&color=FF4B4B" alt="repo-language-count">

<em>Built with the tools and technologies:</em>

<img src="https://img.shields.io/badge/JSON-000000.svg?style=flat-square&logo=JSON&logoColor=white" alt="JSON">
<img src="https://img.shields.io/badge/Unity-FFFFFF.svg?style=flat-square&logo=Unity&logoColor=black" alt="Unity">
<img src="https://custom-icon-badges.demolab.com/badge/C%23-%23239120.svg?logo=cshrp&logoColor=white" alt="C#">
<img src="https://img.shields.io/badge/Oculus-%231A1A1A.svg?logo=oculus&logoColor=white" alt="Oculus">

</div>


<!-- default option, no dependency badges. -->


<!-- default option, no dependency badges. -->

</div>
<br>

---

## Table of Contents

- [Table of Contents](#table-of-contents)
- [Overview](#overview)
- [Features](#features)
- [Project Structure](#project-structure)
    - [Project Index](#project-index)
- [Getting Started](#getting-started)
    - [Prerequisites](#prerequisites)
    - [Installation](#installation)
    - [Usage](#usage)
    - [Testing](#testing)
- [Roadmap](#roadmap)
- [Contributing](#contributing)
- [License](#license)
- [Acknowledgments](#acknowledgments)

---

## Overview
![Hero](readme_materials/00_Hero.png)

<iframe width="560" height="315" src="https://www.youtube.com/embed/DkBCzBZu6SM?si=qwJjYSQA72pXhrDH" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share" referrerpolicy="strict-origin-when-cross-origin" allowfullscreen></iframe>

ARDrum-VirtualHand is the implementation of the micro-progressed AR self-learning drumming system presented within a full paper in
ACM CHI 2025 (https://dl.acm.org/doi/10.1145/3706598.3714156).

The original paper introduced the idea of utilizing the concept of micro-progression to break through the long-standing challenge of using AR to learn/train complex (temporally and physically) skills, drumming, as an example.

As the research and evaluation are detailed in the paper, this document focuses on the system itself. The following sections will cover its features, project structure, and a brief overview of the codebase.

Let’s get started!

---

## Features
Given a specified drumset pattern that the user want to learn as an input.
The system provide 3 feature phases: Record, Playback, Review.
Each phase support corresponding functionalities.

<table>
  <tr>
    <th>Record</th>
    <td>Record playing drum movement.
		<ul>
		<li>Supports different recording speed.</li>
		<li>Can later be playbacked as demonstration or review material.</li>
		</ul>
	</td>
  </tr>
  <tr>
    <th>Playback</th>
    <td>Practice playing the drumset pattern while watching drumming movement demonstrated by virtual limbs.
		<ul>
		<li>Customize the notes that users want to practice or skip, and the demonstration will adjust accordingly.</li>
		<li>Provide real-time visual feedback to users' performance.</li>
		<li>Users' playing drum movement and performance are also recored and used as review material in the review phase.</li>
		</ul>
	</td>
  </tr>
  <th>Review</th>
  <td>Review the performance correctness and compare practice movement with demonstration movement.
  <blockquote>⚠️ Review phase is still under beta testing and is not included in the paper.</blockquote>
  </td>
</table>


---

## Project Structure

```sh
└── ARDrum-VirtualHand/
    ├── Assets
    │   ├── BodyDisplayController.cs
    │   ├── BodyDisplayController.cs.meta
    │   ├── Character
    │   ├── Character.meta
    │   ├── CorrectRateCalculator.cs
    │   ├── CorrectRateCalculator.cs.meta
    │   ├── CorrectRateDisplayer.cs
    │   ├── CorrectRateDisplayer.cs.meta
    │   ├── DisplayControl.cs
    │   ├── DisplayControl.cs.meta
    │   ├── Drum Sheet
    │   ├── Drum Sheet.meta
    │   ├── Drum Video
    │   ├── Drum Video.meta
    │   ├── DrumHitIndicator.cs
    │   ├── DrumHitIndicator.cs.meta
    │   ├── DrumKit.cs
    │   ├── DrumKit.cs.meta
    │   ├── DrumNote.cs
    │   ├── DrumNote.cs.meta
    │   ├── DrumSheet.cs
    │   ├── DrumSheet.cs.meta
    │   ├── DrumSheetCursor.cs
    │   ├── DrumSheetCursor.cs.meta
    │   ├── DrumSheetPlayer.cs
    │   ├── DrumSheetPlayer.cs.meta
    │   ├── DrumStickPath
    │   ├── DrumStickPath.meta
    │   ├── DrumType.cs
    │   ├── DrumType.cs.meta
    │   ├── DrumVideoPlayer.cs
    │   ├── DrumVideoPlayer.cs.meta
    │   ├── Drum_SFX
    │   ├── Drum_SFX.meta
    │   ├── Editor
    │   ├── Editor.meta
    │   ├── Foot Magic Mirror
    │   ├── Foot Magic Mirror.meta
    │   ├── HandFollowController.cs
    │   ├── HandFollowController.cs.meta
    │   ├── HandMovementFeedback.cs
    │   ├── HandMovementFeedback.cs.meta
    │   ├── HandMovementPathRenderer.cs
    │   ├── HandMovementPathRenderer.cs.meta
    │   ├── Level Panel Button
    │   ├── Level Panel Button.meta
    │   ├── LevelController.cs
    │   ├── LevelController.cs.meta
    │   ├── LevelManagementAnnouncement.cs
    │   ├── LevelManagementAnnouncement.cs.meta
    │   ├── LevelManager.cs
    │   ├── LevelManager.cs.meta
    │   ├── Metronome
    │   ├── Metronome.cs
    │   ├── Metronome.cs.meta
    │   ├── Metronome.meta
    │   ├── MetronomeNote.cs
    │   ├── MetronomeNote.cs.meta
    │   ├── Oculus
    │   ├── Oculus.meta
    │   ├── PhaseManager.cs
    │   ├── PhaseManager.cs.meta
    │   ├── PhaseRunner.cs
    │   ├── PhaseRunner.cs.meta
    │   ├── PlayBackPhaseRunner.cs
    │   ├── PlayBackPhaseRunner.cs.meta
    │   ├── PlayBackStatusVisualizer.cs
    │   ├── PlayBackStatusVisualizer.cs.meta
    │   ├── Plugins
    │   ├── Plugins.meta
    │   ├── PracticeRecordPhaseRunner.cs
    │   ├── PracticeRecordPhaseRunner.cs.meta
    │   ├── PracticeRecorder.cs
    │   ├── PracticeRecorder.cs.meta
    │   ├── Readme.asset
    │   ├── Readme.asset.meta
    │   ├── RealTimeInputLogSaver.cs
    │   ├── RealTimeInputLogSaver.cs.meta
    │   ├── RealTimeInputTracker.cs
    │   ├── RealTimeInputTracker.cs.meta
    │   ├── RecordPhaseRunner.cs
    │   ├── RecordPhaseRunner.cs.meta
    │   ├── RecordStatusVisualizer.cs
    │   ├── RecordStatusVisualizer.cs.meta
    │   ├── RecordedPracticeTransforms
    │   ├── RecordedPracticeTransforms.meta
    │   ├── RecordedTransforms
    │   ├── RecordedTransforms.meta
    │   ├── Resources
    │   ├── Resources.meta
    │   ├── ReviewManager.cs
    │   ├── ReviewManager.cs.meta
    │   ├── ReviewPhaseRunner.cs
    │   ├── ReviewPhaseRunner.cs.meta
    │   ├── ReviewSheetDrawer.cs
    │   ├── ReviewSheetDrawer.cs.meta
    │   ├── Samples
    │   ├── Samples.meta
    │   ├── Scenes
    │   ├── Scenes.meta
    │   ├── SetDrumNoteSkipStateButton.cs
    │   ├── SetDrumNoteSkipStateButton.cs.meta
    │   ├── SetHitDrumCorrectMode.cs
    │   ├── SetHitDrumCorrectMode.cs.meta
    │   ├── Settings
    │   ├── Settings.meta
    │   ├── StudyControl.cs
    │   ├── StudyControl.cs.meta
    │   ├── TextMesh Pro
    │   ├── TextMesh Pro.meta
    │   ├── TransformPlayBacker.cs
    │   ├── TransformPlayBacker.cs.meta
    │   ├── TransformRecorder.cs
    │   ├── TransformRecorder.cs.meta
    │   ├── TutorialInfo
    │   ├── TutorialInfo.meta
    │   ├── UniversalRenderPipelineGlobalSettings.asset
    │   ├── UniversalRenderPipelineGlobalSettings.asset.meta
    │   ├── Virtual Drum
    │   ├── Virtual Drum Sheet
    │   ├── Virtual Drum Sheet.meta
    │   ├── Virtual Drum.meta
    │   ├── Virtual Limb Animation
    │   ├── Virtual Limb Animation.meta
    │   ├── Virtual Video Panel
    │   ├── Virtual Video Panel.meta
    │   ├── VirtualDrumController.cs
    │   ├── VirtualDrumController.cs.meta
    │   ├── XR
    │   ├── XR.meta
    │   ├── square.png
    │   └── square.png.meta
    ├── LICENSE
    ├── Packages
    │   ├── manifest.json
    │   └── packages-lock.json
    └── ProjectSettings
        ├── AudioManager.asset
        ├── BurstAotSettings_StandaloneWindows.json
        ├── ClusterInputManager.asset
        ├── CommonBurstAotSettings.json
        ├── DynamicsManager.asset
        ├── EditorBuildSettings.asset
        ├── EditorSettings.asset
        ├── GraphicsSettings.asset
        ├── InputManager.asset
        ├── MemorySettings.asset
        ├── NavMeshAreas.asset
        ├── PackageManagerSettings.asset
        ├── Physics2DSettings.asset
        ├── PresetManager.asset
        ├── ProjectSettings.asset
        ├── ProjectVersion.txt
        ├── QualitySettings.asset
        ├── SceneTemplateSettings.json
        ├── ShaderGraphSettings.asset
        ├── TagManager.asset
        ├── TimeManager.asset
        ├── TimelineSettings.asset
        ├── URPProjectSettings.asset
        ├── UnityConnectSettings.asset
        ├── VFXManager.asset
        ├── VersionControlSettings.asset
        ├── XRPackageSettings.asset
        └── XRSettings.asset
```

### Project Index

<details open>
	<summary><b><code>ARDRUM-VIRTUALHAND/</code></b></summary>
	<!-- __root__ Submodule -->
	<details>
		<summary><b>__root__</b></summary>
		<blockquote>
			<div class='directory-path' style='padding: 8px 0; color: #666;'>
				<code><b>⦿ __root__</b></code>
			<table style='width: 100%; border-collapse: collapse;'>
			<thead>
				<tr style='background-color: #f8f9fa;'>
					<th style='width: 30%; text-align: left; padding: 8px;'>File Name</th>
					<th style='text-align: left; padding: 8px;'>Summary</th>
				</tr>
			</thead>
			</table>
		</blockquote>
	</details>
	<!-- Assets Submodule -->
	<details>
		<summary><b>Assets</b></summary>
		<blockquote>
			<div class='directory-path' style='padding: 8px 0; color: #666;'>
				<code><b>⦿ Assets</b></code>
			<table style='width: 100%; border-collapse: collapse;'>
			<thead>
				<tr style='background-color: #f8f9fa;'>
					<th style='width: 30%; text-align: left; padding: 8px;'>File Name</th>
					<th style='text-align: left; padding: 8px;'>Summary</th>
				</tr>
			</thead>
				<tr style='border-bottom: 1px solid #eee;'>
					<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/SetDrumNoteSkipStateButton.cs'>SetDrumNoteSkipStateButton.cs</a></b></td>
					<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
				</tr>
				<tr style='border-bottom: 1px solid #eee;'>
					<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/LevelManagementAnnouncement.cs'>LevelManagementAnnouncement.cs</a></b></td>
					<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
				</tr>
				<tr style='border-bottom: 1px solid #eee;'>
					<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/HandMovementPathRenderer.cs'>HandMovementPathRenderer.cs</a></b></td>
					<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
				</tr>
				<tr style='border-bottom: 1px solid #eee;'>
					<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/MetronomeNote.cs'>MetronomeNote.cs</a></b></td>
					<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
				</tr>
				<tr style='border-bottom: 1px solid #eee;'>
					<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/ReviewManager.cs'>ReviewManager.cs</a></b></td>
					<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
				</tr>
				<tr style='border-bottom: 1px solid #eee;'>
					<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/TransformRecorder.cs'>TransformRecorder.cs</a></b></td>
					<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
				</tr>
				<tr style='border-bottom: 1px solid #eee;'>
					<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/PracticeRecorder.cs'>PracticeRecorder.cs</a></b></td>
					<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
				</tr>
				<tr style='border-bottom: 1px solid #eee;'>
					<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/CorrectRateCalculator.cs'>CorrectRateCalculator.cs</a></b></td>
					<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
				</tr>
				<tr style='border-bottom: 1px solid #eee;'>
					<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/HandFollowController.cs'>HandFollowController.cs</a></b></td>
					<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
				</tr>
				<tr style='border-bottom: 1px solid #eee;'>
					<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/LevelManager.cs'>LevelManager.cs</a></b></td>
					<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
				</tr>
				<tr style='border-bottom: 1px solid #eee;'>
					<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/PhaseManager.cs'>PhaseManager.cs</a></b></td>
					<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
				</tr>
				<tr style='border-bottom: 1px solid #eee;'>
					<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/VirtualDrumController.cs'>VirtualDrumController.cs</a></b></td>
					<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
				</tr>
				<tr style='border-bottom: 1px solid #eee;'>
					<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/DrumNote.cs'>DrumNote.cs</a></b></td>
					<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
				</tr>
				<tr style='border-bottom: 1px solid #eee;'>
					<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/PlayBackStatusVisualizer.cs'>PlayBackStatusVisualizer.cs</a></b></td>
					<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
				</tr>
				<tr style='border-bottom: 1px solid #eee;'>
					<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/BodyDisplayController.cs'>BodyDisplayController.cs</a></b></td>
					<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
				</tr>
				<tr style='border-bottom: 1px solid #eee;'>
					<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/TransformPlayBacker.cs'>TransformPlayBacker.cs</a></b></td>
					<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
				</tr>
				<tr style='border-bottom: 1px solid #eee;'>
					<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/DrumHitIndicator.cs'>DrumHitIndicator.cs</a></b></td>
					<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
				</tr>
				<tr style='border-bottom: 1px solid #eee;'>
					<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/ReviewSheetDrawer.cs'>ReviewSheetDrawer.cs</a></b></td>
					<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
				</tr>
				<tr style='border-bottom: 1px solid #eee;'>
					<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/CorrectRateDisplayer.cs'>CorrectRateDisplayer.cs</a></b></td>
					<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
				</tr>
				<tr style='border-bottom: 1px solid #eee;'>
					<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/RealTimeInputTracker.cs'>RealTimeInputTracker.cs</a></b></td>
					<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
				</tr>
				<tr style='border-bottom: 1px solid #eee;'>
					<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/RecordPhaseRunner.cs'>RecordPhaseRunner.cs</a></b></td>
					<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
				</tr>
				<tr style='border-bottom: 1px solid #eee;'>
					<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/DrumSheetCursor.cs'>DrumSheetCursor.cs</a></b></td>
					<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
				</tr>
				<tr style='border-bottom: 1px solid #eee;'>
					<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/HandMovementFeedback.cs'>HandMovementFeedback.cs</a></b></td>
					<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
				</tr>
				<tr style='border-bottom: 1px solid #eee;'>
					<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/SetHitDrumCorrectMode.cs'>SetHitDrumCorrectMode.cs</a></b></td>
					<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
				</tr>
				<tr style='border-bottom: 1px solid #eee;'>
					<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/DisplayControl.cs'>DisplayControl.cs</a></b></td>
					<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
				</tr>
				<tr style='border-bottom: 1px solid #eee;'>
					<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/DrumSheetPlayer.cs'>DrumSheetPlayer.cs</a></b></td>
					<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
				</tr>
				<tr style='border-bottom: 1px solid #eee;'>
					<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/ReviewPhaseRunner.cs'>ReviewPhaseRunner.cs</a></b></td>
					<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
				</tr>
				<tr style='border-bottom: 1px solid #eee;'>
					<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/LevelController.cs'>LevelController.cs</a></b></td>
					<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
				</tr>
				<tr style='border-bottom: 1px solid #eee;'>
					<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/RecordStatusVisualizer.cs'>RecordStatusVisualizer.cs</a></b></td>
					<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
				</tr>
				<tr style='border-bottom: 1px solid #eee;'>
					<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/DrumVideoPlayer.cs'>DrumVideoPlayer.cs</a></b></td>
					<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
				</tr>
				<tr style='border-bottom: 1px solid #eee;'>
					<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/PracticeRecordPhaseRunner.cs'>PracticeRecordPhaseRunner.cs</a></b></td>
					<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
				</tr>
				<tr style='border-bottom: 1px solid #eee;'>
					<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/Metronome.cs'>Metronome.cs</a></b></td>
					<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
				</tr>
				<tr style='border-bottom: 1px solid #eee;'>
					<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/DrumKit.cs'>DrumKit.cs</a></b></td>
					<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
				</tr>
				<tr style='border-bottom: 1px solid #eee;'>
					<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/PlayBackPhaseRunner.cs'>PlayBackPhaseRunner.cs</a></b></td>
					<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
				</tr>
				<tr style='border-bottom: 1px solid #eee;'>
					<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/StudyControl.cs'>StudyControl.cs</a></b></td>
					<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
				</tr>
				<tr style='border-bottom: 1px solid #eee;'>
					<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/RealTimeInputLogSaver.cs'>RealTimeInputLogSaver.cs</a></b></td>
					<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
				</tr>
				<tr style='border-bottom: 1px solid #eee;'>
					<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/DrumType.cs'>DrumType.cs</a></b></td>
					<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
				</tr>
				<tr style='border-bottom: 1px solid #eee;'>
					<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/PhaseRunner.cs'>PhaseRunner.cs</a></b></td>
					<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
				</tr>
				<tr style='border-bottom: 1px solid #eee;'>
					<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/DrumSheet.cs'>DrumSheet.cs</a></b></td>
					<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
				</tr>
			</table>
			<!-- Level Panel Button Submodule -->
			<details>
				<summary><b>Level Panel Button</b></summary>
				<blockquote>
					<div class='directory-path' style='padding: 8px 0; color: #666;'>
						<code><b>⦿ Assets.Level Panel Button</b></code>
					<table style='width: 100%; border-collapse: collapse;'>
					<thead>
						<tr style='background-color: #f8f9fa;'>
							<th style='width: 30%; text-align: left; padding: 8px;'>File Name</th>
							<th style='text-align: left; padding: 8px;'>Summary</th>
						</tr>
					</thead>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/Level Panel Button/Level Help Button.mat'>Level Help Button.mat</a></b></td>
							<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
						</tr>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/Level Panel Button/SetDrumNoteSkipStateButton.prefab'>SetDrumNoteSkipStateButton.prefab</a></b></td>
							<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
						</tr>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/Level Panel Button/LevelButton.prefab'>LevelButton.prefab</a></b></td>
							<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
						</tr>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/Level Panel Button/LevelButton.mat'>LevelButton.mat</a></b></td>
							<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
						</tr>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/Level Panel Button/LevelCorrectOrderVerButton.prefab'>LevelCorrectOrderVerButton.prefab</a></b></td>
							<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
						</tr>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/Level Panel Button/SetSkipStateForLimbButton.mat'>SetSkipStateForLimbButton.mat</a></b></td>
							<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
						</tr>
					</table>
				</blockquote>
			</details>
			<!-- Scenes Submodule -->
			<details>
				<summary><b>Scenes</b></summary>
				<blockquote>
					<div class='directory-path' style='padding: 8px 0; color: #666;'>
						<code><b>⦿ Assets.Scenes</b></code>
					<table style='width: 100%; border-collapse: collapse;'>
					<thead>
						<tr style='background-color: #f8f9fa;'>
							<th style='width: 30%; text-align: left; padding: 8px;'>File Name</th>
							<th style='text-align: left; padding: 8px;'>Summary</th>
						</tr>
					</thead>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/Scenes/IK Rig_Controller.unity'>IK Rig_Controller.unity</a></b></td>
							<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
						</tr>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/Scenes/IK Rig_Hand.unity'>IK Rig_Hand.unity</a></b></td>
							<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
						</tr>
					</table>
				</blockquote>
			</details>
			<!-- DrumStickPath Submodule -->
			<details>
				<summary><b>DrumStickPath</b></summary>
				<blockquote>
					<div class='directory-path' style='padding: 8px 0; color: #666;'>
						<code><b>⦿ Assets.DrumStickPath</b></code>
					<table style='width: 100%; border-collapse: collapse;'>
					<thead>
						<tr style='background-color: #f8f9fa;'>
							<th style='width: 30%; text-align: left; padding: 8px;'>File Name</th>
							<th style='text-align: left; padding: 8px;'>Summary</th>
						</tr>
					</thead>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/DrumStickPath/HandMovementPathHighlightPoint_R.mat'>HandMovementPathHighlightPoint_R.mat</a></b></td>
							<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
						</tr>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/DrumStickPath/HandMovementPath_R.mat'>HandMovementPath_R.mat</a></b></td>
							<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
						</tr>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/DrumStickPath/HandMovementPathTrail_L.mat'>HandMovementPathTrail_L.mat</a></b></td>
							<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
						</tr>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/DrumStickPath/HandMovementPathHighlightPoint_L.mat'>HandMovementPathHighlightPoint_L.mat</a></b></td>
							<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
						</tr>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/DrumStickPath/HandMovementPath_L.mat'>HandMovementPath_L.mat</a></b></td>
							<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
						</tr>
					</table>
				</blockquote>
			</details>
			<!-- Foot Magic Mirror Submodule -->
			<details>
				<summary><b>Foot Magic Mirror</b></summary>
				<blockquote>
					<div class='directory-path' style='padding: 8px 0; color: #666;'>
						<code><b>⦿ Assets.Foot Magic Mirror</b></code>
					<table style='width: 100%; border-collapse: collapse;'>
					<thead>
						<tr style='background-color: #f8f9fa;'>
							<th style='width: 30%; text-align: left; padding: 8px;'>File Name</th>
							<th style='text-align: left; padding: 8px;'>Summary</th>
						</tr>
					</thead>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/Foot Magic Mirror/Feet Magic Mirror.renderTexture'>Feet Magic Mirror.renderTexture</a></b></td>
							<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
						</tr>
					</table>
				</blockquote>
			</details>
			<!-- Character Submodule -->
			<details>
				<summary><b>Character</b></summary>
				<blockquote>
					<div class='directory-path' style='padding: 8px 0; color: #666;'>
						<code><b>⦿ Assets.Character</b></code>
					<table style='width: 100%; border-collapse: collapse;'>
					<thead>
						<tr style='background-color: #f8f9fa;'>
							<th style='width: 30%; text-align: left; padding: 8px;'>File Name</th>
							<th style='text-align: left; padding: 8px;'>Summary</th>
						</tr>
					</thead>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/Character/Banana Man.prefab'>Banana Man.prefab</a></b></td>
							<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
						</tr>
					</table>
					<!-- Banana Man Model Submodule -->
					<details>
						<summary><b>Banana Man Model</b></summary>
						<blockquote>
							<div class='directory-path' style='padding: 8px 0; color: #666;'>
								<code><b>⦿ Assets.Character.Banana Man Model</b></code>
							<!-- Banana Yellow Games Submodule -->
							<details>
								<summary><b>Banana Yellow Games</b></summary>
								<blockquote>
									<div class='directory-path' style='padding: 8px 0; color: #666;'>
										<code><b>⦿ Assets.Character.Banana Man Model.Banana Yellow Games</b></code>
									<!-- Characters Submodule -->
									<details>
										<summary><b>Characters</b></summary>
										<blockquote>
											<div class='directory-path' style='padding: 8px 0; color: #666;'>
												<code><b>⦿ Assets.Character.Banana Man Model.Banana Yellow Games.Characters</b></code>
											<!-- Banana Man Submodule -->
											<details>
												<summary><b>Banana Man</b></summary>
												<blockquote>
													<div class='directory-path' style='padding: 8px 0; color: #666;'>
														<code><b>⦿ Assets.Character.Banana Man Model.Banana Yellow Games.Characters.Banana Man</b></code>
													<table style='width: 100%; border-collapse: collapse;'>
													<thead>
														<tr style='background-color: #f8f9fa;'>
															<th style='width: 30%; text-align: left; padding: 8px;'>File Name</th>
															<th style='text-align: left; padding: 8px;'>Summary</th>
														</tr>
													</thead>
														<tr style='border-bottom: 1px solid #eee;'>
															<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/Character/Banana Man Model/Banana Yellow Games/Characters/Banana Man/Banana Man.fbx'>Banana Man.fbx</a></b></td>
															<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
														</tr>
													</table>
													<!-- Materials Submodule -->
													<details>
														<summary><b>Materials</b></summary>
														<blockquote>
															<div class='directory-path' style='padding: 8px 0; color: #666;'>
																<code><b>⦿ Assets.Character.Banana Man Model.Banana Yellow Games.Characters.Banana Man.Materials</b></code>
															<table style='width: 100%; border-collapse: collapse;'>
															<thead>
																<tr style='background-color: #f8f9fa;'>
																	<th style='width: 30%; text-align: left; padding: 8px;'>File Name</th>
																	<th style='text-align: left; padding: 8px;'>Summary</th>
																</tr>
															</thead>
																<tr style='border-bottom: 1px solid #eee;'>
																	<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/Character/Banana Man Model/Banana Yellow Games/Characters/Banana Man/Materials/Joints 1.mat'>Joints 1.mat</a></b></td>
																	<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
																</tr>
																<tr style='border-bottom: 1px solid #eee;'>
																	<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/Character/Banana Man Model/Banana Yellow Games/Characters/Banana Man/Materials/Body 1.mat'>Body 1.mat</a></b></td>
																	<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
																</tr>
																<tr style='border-bottom: 1px solid #eee;'>
																	<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/Character/Banana Man Model/Banana Yellow Games/Characters/Banana Man/Materials/Joints.mat'>Joints.mat</a></b></td>
																	<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
																</tr>
																<tr style='border-bottom: 1px solid #eee;'>
																	<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/Character/Banana Man Model/Banana Yellow Games/Characters/Banana Man/Materials/Body.mat'>Body.mat</a></b></td>
																	<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
																</tr>
															</table>
														</blockquote>
													</details>
													<!-- Demo Scene Submodule -->
													<details>
														<summary><b>Demo Scene</b></summary>
														<blockquote>
															<div class='directory-path' style='padding: 8px 0; color: #666;'>
																<code><b>⦿ Assets.Character.Banana Man Model.Banana Yellow Games.Characters.Banana Man.Demo Scene</b></code>
															<table style='width: 100%; border-collapse: collapse;'>
															<thead>
																<tr style='background-color: #f8f9fa;'>
																	<th style='width: 30%; text-align: left; padding: 8px;'>File Name</th>
																	<th style='text-align: left; padding: 8px;'>Summary</th>
																</tr>
															</thead>
																<tr style='border-bottom: 1px solid #eee;'>
																	<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/Character/Banana Man Model/Banana Yellow Games/Characters/Banana Man/Demo Scene/Banana Man Demo Scene.unity'>Banana Man Demo Scene.unity</a></b></td>
																	<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
																</tr>
																<tr style='border-bottom: 1px solid #eee;'>
																	<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/Character/Banana Man Model/Banana Yellow Games/Characters/Banana Man/Demo Scene/Gray Skybox.mat'>Gray Skybox.mat</a></b></td>
																	<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
																</tr>
															</table>
														</blockquote>
													</details>
												</blockquote>
											</details>
										</blockquote>
									</details>
								</blockquote>
							</details>
						</blockquote>
					</details>
					<!-- Banana Man V2 Model Submodule -->
					<details>
						<summary><b>Banana Man V2 Model</b></summary>
						<blockquote>
							<div class='directory-path' style='padding: 8px 0; color: #666;'>
								<code><b>⦿ Assets.Character.Banana Man V2 Model</b></code>
							<table style='width: 100%; border-collapse: collapse;'>
							<thead>
								<tr style='background-color: #f8f9fa;'>
									<th style='width: 30%; text-align: left; padding: 8px;'>File Name</th>
									<th style='text-align: left; padding: 8px;'>Summary</th>
								</tr>
							</thead>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/Character/Banana Man V2 Model/Bananaman V2.fbx'>Bananaman V2.fbx</a></b></td>
									<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
								</tr>
							</table>
							<!-- Materials Submodule -->
							<details>
								<summary><b>Materials</b></summary>
								<blockquote>
									<div class='directory-path' style='padding: 8px 0; color: #666;'>
										<code><b>⦿ Assets.Character.Banana Man V2 Model.Materials</b></code>
									<table style='width: 100%; border-collapse: collapse;'>
									<thead>
										<tr style='background-color: #f8f9fa;'>
											<th style='width: 30%; text-align: left; padding: 8px;'>File Name</th>
											<th style='text-align: left; padding: 8px;'>Summary</th>
										</tr>
									</thead>
										<tr style='border-bottom: 1px solid #eee;'>
											<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/Character/Banana Man V2 Model/Materials/Joints.mat'>Joints.mat</a></b></td>
											<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
										</tr>
										<tr style='border-bottom: 1px solid #eee;'>
											<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/Character/Banana Man V2 Model/Materials/Body.mat'>Body.mat</a></b></td>
											<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
										</tr>
									</table>
									<!-- Joints Submodule -->
									<details>
										<summary><b>Joints</b></summary>
										<blockquote>
											<div class='directory-path' style='padding: 8px 0; color: #666;'>
												<code><b>⦿ Assets.Character.Banana Man V2 Model.Materials.Joints</b></code>
											<table style='width: 100%; border-collapse: collapse;'>
											<thead>
												<tr style='background-color: #f8f9fa;'>
													<th style='width: 30%; text-align: left; padding: 8px;'>File Name</th>
													<th style='text-align: left; padding: 8px;'>Summary</th>
												</tr>
											</thead>
												<tr style='border-bottom: 1px solid #eee;'>
													<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/Character/Banana Man V2 Model/Materials/Joints/Hand Joint R.mat'>Hand Joint R.mat</a></b></td>
													<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
												</tr>
												<tr style='border-bottom: 1px solid #eee;'>
													<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/Character/Banana Man V2 Model/Materials/Joints/Knee Joint.mat'>Knee Joint.mat</a></b></td>
													<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
												</tr>
												<tr style='border-bottom: 1px solid #eee;'>
													<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/Character/Banana Man V2 Model/Materials/Joints/Elbow Joint L.mat'>Elbow Joint L.mat</a></b></td>
													<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
												</tr>
												<tr style='border-bottom: 1px solid #eee;'>
													<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/Character/Banana Man V2 Model/Materials/Joints/Feet Joint.mat'>Feet Joint.mat</a></b></td>
													<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
												</tr>
												<tr style='border-bottom: 1px solid #eee;'>
													<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/Character/Banana Man V2 Model/Materials/Joints/Shoulder Joint R.mat'>Shoulder Joint R.mat</a></b></td>
													<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
												</tr>
												<tr style='border-bottom: 1px solid #eee;'>
													<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/Character/Banana Man V2 Model/Materials/Joints/Wrist Joint R.mat'>Wrist Joint R.mat</a></b></td>
													<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
												</tr>
												<tr style='border-bottom: 1px solid #eee;'>
													<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/Character/Banana Man V2 Model/Materials/Joints/Neck & Spine Joint.mat'>Neck & Spine Joint.mat</a></b></td>
													<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
												</tr>
												<tr style='border-bottom: 1px solid #eee;'>
													<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/Character/Banana Man V2 Model/Materials/Joints/Shoulder Joint L.mat'>Shoulder Joint L.mat</a></b></td>
													<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
												</tr>
												<tr style='border-bottom: 1px solid #eee;'>
													<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/Character/Banana Man V2 Model/Materials/Joints/Wrist Joint L.mat'>Wrist Joint L.mat</a></b></td>
													<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
												</tr>
												<tr style='border-bottom: 1px solid #eee;'>
													<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/Character/Banana Man V2 Model/Materials/Joints/Hand Joint L.mat'>Hand Joint L.mat</a></b></td>
													<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
												</tr>
												<tr style='border-bottom: 1px solid #eee;'>
													<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/Character/Banana Man V2 Model/Materials/Joints/Elbow Joint R.mat'>Elbow Joint R.mat</a></b></td>
													<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
												</tr>
												<tr style='border-bottom: 1px solid #eee;'>
													<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/Character/Banana Man V2 Model/Materials/Joints/Thight Joint.mat'>Thight Joint.mat</a></b></td>
													<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
												</tr>
											</table>
										</blockquote>
									</details>
									<!-- Bodys Submodule -->
									<details>
										<summary><b>Bodys</b></summary>
										<blockquote>
											<div class='directory-path' style='padding: 8px 0; color: #666;'>
												<code><b>⦿ Assets.Character.Banana Man V2 Model.Materials.Bodys</b></code>
											<table style='width: 100%; border-collapse: collapse;'>
											<thead>
												<tr style='background-color: #f8f9fa;'>
													<th style='width: 30%; text-align: left; padding: 8px;'>File Name</th>
													<th style='text-align: left; padding: 8px;'>Summary</th>
												</tr>
											</thead>
												<tr style='border-bottom: 1px solid #eee;'>
													<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/Character/Banana Man V2 Model/Materials/Bodys/Chest.mat'>Chest.mat</a></b></td>
													<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
												</tr>
												<tr style='border-bottom: 1px solid #eee;'>
													<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/Character/Banana Man V2 Model/Materials/Bodys/ForeArm_L.mat'>ForeArm_L.mat</a></b></td>
													<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
												</tr>
												<tr style='border-bottom: 1px solid #eee;'>
													<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/Character/Banana Man V2 Model/Materials/Bodys/DrumStick_L.mat'>DrumStick_L.mat</a></b></td>
													<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
												</tr>
												<tr style='border-bottom: 1px solid #eee;'>
													<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/Character/Banana Man V2 Model/Materials/Bodys/UpperArm_L.mat'>UpperArm_L.mat</a></b></td>
													<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
												</tr>
												<tr style='border-bottom: 1px solid #eee;'>
													<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/Character/Banana Man V2 Model/Materials/Bodys/Feet.mat'>Feet.mat</a></b></td>
													<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
												</tr>
												<tr style='border-bottom: 1px solid #eee;'>
													<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/Character/Banana Man V2 Model/Materials/Bodys/Hand_L.mat'>Hand_L.mat</a></b></td>
													<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
												</tr>
												<tr style='border-bottom: 1px solid #eee;'>
													<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/Character/Banana Man V2 Model/Materials/Bodys/Stomach.mat'>Stomach.mat</a></b></td>
													<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
												</tr>
												<tr style='border-bottom: 1px solid #eee;'>
													<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/Character/Banana Man V2 Model/Materials/Bodys/Thight.mat'>Thight.mat</a></b></td>
													<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
												</tr>
												<tr style='border-bottom: 1px solid #eee;'>
													<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/Character/Banana Man V2 Model/Materials/Bodys/Hand_R.mat'>Hand_R.mat</a></b></td>
													<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
												</tr>
												<tr style='border-bottom: 1px solid #eee;'>
													<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/Character/Banana Man V2 Model/Materials/Bodys/Head.mat'>Head.mat</a></b></td>
													<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
												</tr>
												<tr style='border-bottom: 1px solid #eee;'>
													<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/Character/Banana Man V2 Model/Materials/Bodys/Hip.mat'>Hip.mat</a></b></td>
													<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
												</tr>
												<tr style='border-bottom: 1px solid #eee;'>
													<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/Character/Banana Man V2 Model/Materials/Bodys/Leg.mat'>Leg.mat</a></b></td>
													<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
												</tr>
												<tr style='border-bottom: 1px solid #eee;'>
													<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/Character/Banana Man V2 Model/Materials/Bodys/ForeArm_R.mat'>ForeArm_R.mat</a></b></td>
													<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
												</tr>
												<tr style='border-bottom: 1px solid #eee;'>
													<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/Character/Banana Man V2 Model/Materials/Bodys/DrumStick_R.mat'>DrumStick_R.mat</a></b></td>
													<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
												</tr>
												<tr style='border-bottom: 1px solid #eee;'>
													<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/Character/Banana Man V2 Model/Materials/Bodys/UpperArm_R.mat'>UpperArm_R.mat</a></b></td>
													<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
												</tr>
											</table>
										</blockquote>
									</details>
								</blockquote>
							</details>
						</blockquote>
					</details>
				</blockquote>
			</details>
			<!-- RecordedTransforms Submodule -->
			<details>
				<summary><b>RecordedTransforms</b></summary>
				<blockquote>
					<div class='directory-path' style='padding: 8px 0; color: #666;'>
						<code><b>⦿ Assets.RecordedTransforms</b></code>
					<table style='width: 100%; border-collapse: collapse;'>
					<thead>
						<tr style='background-color: #f8f9fa;'>
							<th style='width: 30%; text-align: left; padding: 8px;'>File Name</th>
							<th style='text-align: left; padding: 8px;'>Summary</th>
						</tr>
					</thead>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/RecordedTransforms/3_advanced2.json'>3_advanced2.json</a></b></td>
							<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
						</tr>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/RecordedTransforms/7_test_cont.json'>7_test_cont.json</a></b></td>
							<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
						</tr>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/RecordedTransforms/8_test_cont.json'>8_test_cont.json</a></b></td>
							<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
						</tr>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/RecordedTransforms/medium2.json'>medium2.json</a></b></td>
							<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
						</tr>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/RecordedTransforms/medium3.json'>medium3.json</a></b></td>
							<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
						</tr>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/RecordedTransforms/smid2.json'>smid2.json</a></b></td>
							<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
						</tr>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/RecordedTransforms/easy3 copy.json'>easy3 copy.json</a></b></td>
							<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
						</tr>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/RecordedTransforms/6_1e2a3e4a_same_cont.json'>6_1e2a3e4a_same_cont.json</a></b></td>
							<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
						</tr>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/RecordedTransforms/easy2.json'>easy2.json</a></b></td>
							<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
						</tr>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/RecordedTransforms/5_1e2a3e4a_cont.json'>5_1e2a3e4a_cont.json</a></b></td>
							<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
						</tr>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/RecordedTransforms/easy3.json'>easy3.json</a></b></td>
							<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
						</tr>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/RecordedTransforms/0_1234.json'>0_1234.json</a></b></td>
							<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
						</tr>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/RecordedTransforms/easy3_unmodified_time.json'>easy3_unmodified_time.json</a></b></td>
							<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
						</tr>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/RecordedTransforms/2_2a4a.json'>2_2a4a.json</a></b></td>
							<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
						</tr>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/RecordedTransforms/1_1e3e.json'>1_1e3e.json</a></b></td>
							<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
						</tr>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/RecordedTransforms/medium3 copy.json'>medium3 copy.json</a></b></td>
							<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
						</tr>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/RecordedTransforms/easy1.json'>easy1.json</a></b></td>
							<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
						</tr>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/RecordedTransforms/sample.json'>sample.json</a></b></td>
							<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
						</tr>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/RecordedTransforms/mid2_cleaning_halfway_RH4.json'>mid2_cleaning_halfway_RH4.json</a></b></td>
							<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
						</tr>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/RecordedTransforms/4_1e2a3e4a.json'>4_1e2a3e4a.json</a></b></td>
							<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
						</tr>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/RecordedTransforms/medium1.json'>medium1.json</a></b></td>
							<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
						</tr>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/RecordedTransforms/mid2.json'>mid2.json</a></b></td>
							<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
						</tr>
					</table>
					<!-- 2-1 Submodule -->
					<details>
						<summary><b>2-1</b></summary>
						<blockquote>
							<div class='directory-path' style='padding: 8px 0; color: #666;'>
								<code><b>⦿ Assets.RecordedTransforms.2-1</b></code>
							<table style='width: 100%; border-collapse: collapse;'>
							<thead>
								<tr style='background-color: #f8f9fa;'>
									<th style='width: 30%; text-align: left; padding: 8px;'>File Name</th>
									<th style='text-align: left; padding: 8px;'>Summary</th>
								</tr>
							</thead>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/RecordedTransforms/2-1/0.json'>0.json</a></b></td>
									<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
								</tr>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/RecordedTransforms/2-1/1.json'>1.json</a></b></td>
									<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
								</tr>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/RecordedTransforms/2-1/2.json'>2.json</a></b></td>
									<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
								</tr>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/RecordedTransforms/2-1/3.json'>3.json</a></b></td>
									<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
								</tr>
							</table>
						</blockquote>
					</details>
					<!-- 0 Submodule -->
					<details>
						<summary><b>0</b></summary>
						<blockquote>
							<div class='directory-path' style='padding: 8px 0; color: #666;'>
								<code><b>⦿ Assets.RecordedTransforms.0</b></code>
							<table style='width: 100%; border-collapse: collapse;'>
							<thead>
								<tr style='background-color: #f8f9fa;'>
									<th style='width: 30%; text-align: left; padding: 8px;'>File Name</th>
									<th style='text-align: left; padding: 8px;'>Summary</th>
								</tr>
							</thead>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/RecordedTransforms/0/0_2.json'>0_2.json</a></b></td>
									<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
								</tr>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/RecordedTransforms/0/0.json'>0.json</a></b></td>
									<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
								</tr>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/RecordedTransforms/0/1.json'>1.json</a></b></td>
									<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
								</tr>
							</table>
						</blockquote>
					</details>
					<!-- 7 Submodule -->
					<details>
						<summary><b>7</b></summary>
						<blockquote>
							<div class='directory-path' style='padding: 8px 0; color: #666;'>
								<code><b>⦿ Assets.RecordedTransforms.7</b></code>
							<table style='width: 100%; border-collapse: collapse;'>
							<thead>
								<tr style='background-color: #f8f9fa;'>
									<th style='width: 30%; text-align: left; padding: 8px;'>File Name</th>
									<th style='text-align: left; padding: 8px;'>Summary</th>
								</tr>
							</thead>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/RecordedTransforms/7/0.json'>0.json</a></b></td>
									<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
								</tr>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/RecordedTransforms/7/1.json'>1.json</a></b></td>
									<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
								</tr>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/RecordedTransforms/7/2.json'>2.json</a></b></td>
									<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
								</tr>
							</table>
						</blockquote>
					</details>
					<!-- 1 Submodule -->
					<details>
						<summary><b>1</b></summary>
						<blockquote>
							<div class='directory-path' style='padding: 8px 0; color: #666;'>
								<code><b>⦿ Assets.RecordedTransforms.1</b></code>
							<table style='width: 100%; border-collapse: collapse;'>
							<thead>
								<tr style='background-color: #f8f9fa;'>
									<th style='width: 30%; text-align: left; padding: 8px;'>File Name</th>
									<th style='text-align: left; padding: 8px;'>Summary</th>
								</tr>
							</thead>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/RecordedTransforms/1/6.json'>6.json</a></b></td>
									<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
								</tr>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/RecordedTransforms/1/7.json'>7.json</a></b></td>
									<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
								</tr>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/RecordedTransforms/1/3.json'>3.json</a></b></td>
									<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
								</tr>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/RecordedTransforms/1/8.json'>8.json</a></b></td>
									<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
								</tr>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/RecordedTransforms/1/4.json'>4.json</a></b></td>
									<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
								</tr>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/RecordedTransforms/1/5.json'>5.json</a></b></td>
									<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
								</tr>
							</table>
						</blockquote>
					</details>
					<!-- 4-2 Submodule -->
					<details>
						<summary><b>4-2</b></summary>
						<blockquote>
							<div class='directory-path' style='padding: 8px 0; color: #666;'>
								<code><b>⦿ Assets.RecordedTransforms.4-2</b></code>
							<table style='width: 100%; border-collapse: collapse;'>
							<thead>
								<tr style='background-color: #f8f9fa;'>
									<th style='width: 30%; text-align: left; padding: 8px;'>File Name</th>
									<th style='text-align: left; padding: 8px;'>Summary</th>
								</tr>
							</thead>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/RecordedTransforms/4-2/0.json'>0.json</a></b></td>
									<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
								</tr>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/RecordedTransforms/4-2/1.json'>1.json</a></b></td>
									<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
								</tr>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/RecordedTransforms/4-2/2.json'>2.json</a></b></td>
									<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
								</tr>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/RecordedTransforms/4-2/3.json'>3.json</a></b></td>
									<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
								</tr>
							</table>
						</blockquote>
					</details>
					<!-- 6-1 Submodule -->
					<details>
						<summary><b>6-1</b></summary>
						<blockquote>
							<div class='directory-path' style='padding: 8px 0; color: #666;'>
								<code><b>⦿ Assets.RecordedTransforms.6-1</b></code>
							<table style='width: 100%; border-collapse: collapse;'>
							<thead>
								<tr style='background-color: #f8f9fa;'>
									<th style='width: 30%; text-align: left; padding: 8px;'>File Name</th>
									<th style='text-align: left; padding: 8px;'>Summary</th>
								</tr>
							</thead>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/RecordedTransforms/6-1/0.json'>0.json</a></b></td>
									<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
								</tr>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/RecordedTransforms/6-1/1.json'>1.json</a></b></td>
									<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
								</tr>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/RecordedTransforms/6-1/2.json'>2.json</a></b></td>
									<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
								</tr>
							</table>
						</blockquote>
					</details>
					<!-- 6-3 Submodule -->
					<details>
						<summary><b>6-3</b></summary>
						<blockquote>
							<div class='directory-path' style='padding: 8px 0; color: #666;'>
								<code><b>⦿ Assets.RecordedTransforms.6-3</b></code>
							<table style='width: 100%; border-collapse: collapse;'>
							<thead>
								<tr style='background-color: #f8f9fa;'>
									<th style='width: 30%; text-align: left; padding: 8px;'>File Name</th>
									<th style='text-align: left; padding: 8px;'>Summary</th>
								</tr>
							</thead>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/RecordedTransforms/6-3/0.json'>0.json</a></b></td>
									<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
								</tr>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/RecordedTransforms/6-3/1.json'>1.json</a></b></td>
									<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
								</tr>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/RecordedTransforms/6-3/2.json'>2.json</a></b></td>
									<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
								</tr>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/RecordedTransforms/6-3/3.json'>3.json</a></b></td>
									<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
								</tr>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/RecordedTransforms/6-3/4.json'>4.json</a></b></td>
									<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
								</tr>
							</table>
						</blockquote>
					</details>
					<!-- 2-2 Submodule -->
					<details>
						<summary><b>2-2</b></summary>
						<blockquote>
							<div class='directory-path' style='padding: 8px 0; color: #666;'>
								<code><b>⦿ Assets.RecordedTransforms.2-2</b></code>
							<table style='width: 100%; border-collapse: collapse;'>
							<thead>
								<tr style='background-color: #f8f9fa;'>
									<th style='width: 30%; text-align: left; padding: 8px;'>File Name</th>
									<th style='text-align: left; padding: 8px;'>Summary</th>
								</tr>
							</thead>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/RecordedTransforms/2-2/6.json'>6.json</a></b></td>
									<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
								</tr>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/RecordedTransforms/2-2/7.json'>7.json</a></b></td>
									<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
								</tr>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/RecordedTransforms/2-2/4.json'>4.json</a></b></td>
									<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
								</tr>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/RecordedTransforms/2-2/5.json'>5.json</a></b></td>
									<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
								</tr>
							</table>
						</blockquote>
					</details>
					<!-- 3 Submodule -->
					<details>
						<summary><b>3</b></summary>
						<blockquote>
							<div class='directory-path' style='padding: 8px 0; color: #666;'>
								<code><b>⦿ Assets.RecordedTransforms.3</b></code>
							<table style='width: 100%; border-collapse: collapse;'>
							<thead>
								<tr style='background-color: #f8f9fa;'>
									<th style='width: 30%; text-align: left; padding: 8px;'>File Name</th>
									<th style='text-align: left; padding: 8px;'>Summary</th>
								</tr>
							</thead>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/RecordedTransforms/3/6.json'>6.json</a></b></td>
									<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
								</tr>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/RecordedTransforms/3/7.json'>7.json</a></b></td>
									<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
								</tr>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/RecordedTransforms/3/0.json'>0.json</a></b></td>
									<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
								</tr>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/RecordedTransforms/3/1.json'>1.json</a></b></td>
									<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
								</tr>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/RecordedTransforms/3/2.json'>2.json</a></b></td>
									<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
								</tr>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/RecordedTransforms/3/3.json'>3.json</a></b></td>
									<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
								</tr>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/RecordedTransforms/3/8.json'>8.json</a></b></td>
									<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
								</tr>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/RecordedTransforms/3/4.json'>4.json</a></b></td>
									<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
								</tr>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/RecordedTransforms/3/5.json'>5.json</a></b></td>
									<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
								</tr>
							</table>
						</blockquote>
					</details>
					<!-- 4-1 Submodule -->
					<details>
						<summary><b>4-1</b></summary>
						<blockquote>
							<div class='directory-path' style='padding: 8px 0; color: #666;'>
								<code><b>⦿ Assets.RecordedTransforms.4-1</b></code>
							<table style='width: 100%; border-collapse: collapse;'>
							<thead>
								<tr style='background-color: #f8f9fa;'>
									<th style='width: 30%; text-align: left; padding: 8px;'>File Name</th>
									<th style='text-align: left; padding: 8px;'>Summary</th>
								</tr>
							</thead>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/RecordedTransforms/4-1/6.json'>6.json</a></b></td>
									<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
								</tr>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/RecordedTransforms/4-1/1.json'>1.json</a></b></td>
									<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
								</tr>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/RecordedTransforms/4-1/2.json'>2.json</a></b></td>
									<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
								</tr>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/RecordedTransforms/4-1/3.json'>3.json</a></b></td>
									<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
								</tr>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/RecordedTransforms/4-1/4.json'>4.json</a></b></td>
									<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
								</tr>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/RecordedTransforms/4-1/5.json'>5.json</a></b></td>
									<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
								</tr>
							</table>
						</blockquote>
					</details>
					<!-- 6-2 Submodule -->
					<details>
						<summary><b>6-2</b></summary>
						<blockquote>
							<div class='directory-path' style='padding: 8px 0; color: #666;'>
								<code><b>⦿ Assets.RecordedTransforms.6-2</b></code>
							<table style='width: 100%; border-collapse: collapse;'>
							<thead>
								<tr style='background-color: #f8f9fa;'>
									<th style='width: 30%; text-align: left; padding: 8px;'>File Name</th>
									<th style='text-align: left; padding: 8px;'>Summary</th>
								</tr>
							</thead>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/RecordedTransforms/6-2/0.json'>0.json</a></b></td>
									<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
								</tr>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/RecordedTransforms/6-2/1.json'>1.json</a></b></td>
									<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
								</tr>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/RecordedTransforms/6-2/2.json'>2.json</a></b></td>
									<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
								</tr>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/RecordedTransforms/6-2/3.json'>3.json</a></b></td>
									<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
								</tr>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/RecordedTransforms/6-2/4.json'>4.json</a></b></td>
									<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
								</tr>
							</table>
						</blockquote>
					</details>
					<!-- 5 Submodule -->
					<details>
						<summary><b>5</b></summary>
						<blockquote>
							<div class='directory-path' style='padding: 8px 0; color: #666;'>
								<code><b>⦿ Assets.RecordedTransforms.5</b></code>
							<table style='width: 100%; border-collapse: collapse;'>
							<thead>
								<tr style='background-color: #f8f9fa;'>
									<th style='width: 30%; text-align: left; padding: 8px;'>File Name</th>
									<th style='text-align: left; padding: 8px;'>Summary</th>
								</tr>
							</thead>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/RecordedTransforms/5/0.json'>0.json</a></b></td>
									<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
								</tr>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/RecordedTransforms/5/1.json'>1.json</a></b></td>
									<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
								</tr>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/RecordedTransforms/5/2.json'>2.json</a></b></td>
									<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
								</tr>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/RecordedTransforms/5/3.json'>3.json</a></b></td>
									<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
								</tr>
							</table>
						</blockquote>
					</details>
					<!-- 2-3 Submodule -->
					<details>
						<summary><b>2-3</b></summary>
						<blockquote>
							<div class='directory-path' style='padding: 8px 0; color: #666;'>
								<code><b>⦿ Assets.RecordedTransforms.2-3</b></code>
							<table style='width: 100%; border-collapse: collapse;'>
							<thead>
								<tr style='background-color: #f8f9fa;'>
									<th style='width: 30%; text-align: left; padding: 8px;'>File Name</th>
									<th style='text-align: left; padding: 8px;'>Summary</th>
								</tr>
							</thead>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/RecordedTransforms/2-3/0.json'>0.json</a></b></td>
									<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
								</tr>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/RecordedTransforms/2-3/1.json'>1.json</a></b></td>
									<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
								</tr>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/RecordedTransforms/2-3/2.json'>2.json</a></b></td>
									<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
								</tr>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/RecordedTransforms/2-3/3.json'>3.json</a></b></td>
									<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
								</tr>
								<tr style='border-bottom: 1px solid #eee;'>
									<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/RecordedTransforms/2-3/4.json'>4.json</a></b></td>
									<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
								</tr>
							</table>
						</blockquote>
					</details>
				</blockquote>
			</details>
			<!-- Virtual Video Panel Submodule -->
			<details>
				<summary><b>Virtual Video Panel</b></summary>
				<blockquote>
					<div class='directory-path' style='padding: 8px 0; color: #666;'>
						<code><b>⦿ Assets.Virtual Video Panel</b></code>
					<table style='width: 100%; border-collapse: collapse;'>
					<thead>
						<tr style='background-color: #f8f9fa;'>
							<th style='width: 30%; text-align: left; padding: 8px;'>File Name</th>
							<th style='text-align: left; padding: 8px;'>Summary</th>
						</tr>
					</thead>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/Virtual Video Panel/Drum Video.renderTexture'>Drum Video.renderTexture</a></b></td>
							<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
						</tr>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/Virtual Video Panel/Drum Video.mat'>Drum Video.mat</a></b></td>
							<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
						</tr>
					</table>
				</blockquote>
			</details>
			<!-- Virtual Drum Sheet Submodule -->
			<details>
				<summary><b>Virtual Drum Sheet</b></summary>
				<blockquote>
					<div class='directory-path' style='padding: 8px 0; color: #666;'>
						<code><b>⦿ Assets.Virtual Drum Sheet</b></code>
					<table style='width: 100%; border-collapse: collapse;'>
					<thead>
						<tr style='background-color: #f8f9fa;'>
							<th style='width: 30%; text-align: left; padding: 8px;'>File Name</th>
							<th style='text-align: left; padding: 8px;'>Summary</th>
						</tr>
					</thead>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/Virtual Drum Sheet/DrumNote Sphere.prefab'>DrumNote Sphere.prefab</a></b></td>
							<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
						</tr>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/Virtual Drum Sheet/HitDrumInputLevel1ErrorMarker.prefab'>HitDrumInputLevel1ErrorMarker.prefab</a></b></td>
							<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
						</tr>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/Virtual Drum Sheet/HitDrumInputMissMarker.prefab'>HitDrumInputMissMarker.prefab</a></b></td>
							<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
						</tr>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/Virtual Drum Sheet/HitDrumInputMissMarker.mat'>HitDrumInputMissMarker.mat</a></b></td>
							<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
						</tr>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/Virtual Drum Sheet/DrumSheet.mat'>DrumSheet.mat</a></b></td>
							<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
						</tr>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/Virtual Drum Sheet/DrumSheetCursor.mat'>DrumSheetCursor.mat</a></b></td>
							<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
						</tr>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/Virtual Drum Sheet/DrumNote.mat'>DrumNote.mat</a></b></td>
							<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
						</tr>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/Virtual Drum Sheet/HandMovementPathTrail_R.mat'>HandMovementPathTrail_R.mat</a></b></td>
							<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
						</tr>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/Virtual Drum Sheet/HitDrumInputCrossMarker.prefab'>HitDrumInputCrossMarker.prefab</a></b></td>
							<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
						</tr>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/Virtual Drum Sheet/HitDrumInputLevel1ErrorMarker.mat'>HitDrumInputLevel1ErrorMarker.mat</a></b></td>
							<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
						</tr>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/Virtual Drum Sheet/ReviewSheetNote.mat'>ReviewSheetNote.mat</a></b></td>
							<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
						</tr>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/Virtual Drum Sheet/HitDrumInputCorrectMarker.prefab'>HitDrumInputCorrectMarker.prefab</a></b></td>
							<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
						</tr>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/Virtual Drum Sheet/HitDrumInputCorrectMarker.mat'>HitDrumInputCorrectMarker.mat</a></b></td>
							<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
						</tr>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/Virtual Drum Sheet/HitDrumInputCrossMarker.mat'>HitDrumInputCrossMarker.mat</a></b></td>
							<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
						</tr>
					</table>
				</blockquote>
			</details>
			<!-- Virtual Drum Submodule -->
			<details>
				<summary><b>Virtual Drum</b></summary>
				<blockquote>
					<div class='directory-path' style='padding: 8px 0; color: #666;'>
						<code><b>⦿ Assets.Virtual Drum</b></code>
					<table style='width: 100%; border-collapse: collapse;'>
					<thead>
						<tr style='background-color: #f8f9fa;'>
							<th style='width: 30%; text-align: left; padding: 8px;'>File Name</th>
							<th style='text-align: left; padding: 8px;'>Summary</th>
						</tr>
					</thead>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/Virtual Drum/Transparent Sphere.mat'>Transparent Sphere.mat</a></b></td>
							<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
						</tr>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/Virtual Drum/Virtual Drum.mat'>Virtual Drum.mat</a></b></td>
							<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
						</tr>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/Virtual Drum/Transparent Circle.mat'>Transparent Circle.mat</a></b></td>
							<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
						</tr>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/Virtual Drum/Virtual Drum Bass.mat'>Virtual Drum Bass.mat</a></b></td>
							<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
						</tr>
					</table>
				</blockquote>
			</details>
			<!-- RecordedPracticeTransforms Submodule -->
			<details>
				<summary><b>RecordedPracticeTransforms</b></summary>
				<blockquote>
					<div class='directory-path' style='padding: 8px 0; color: #666;'>
						<code><b>⦿ Assets.RecordedPracticeTransforms</b></code>
					<table style='width: 100%; border-collapse: collapse;'>
					<thead>
						<tr style='background-color: #f8f9fa;'>
							<th style='width: 30%; text-align: left; padding: 8px;'>File Name</th>
							<th style='text-align: left; padding: 8px;'>Summary</th>
						</tr>
					</thead>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/RecordedPracticeTransforms/0_practice.json'>0_practice.json</a></b></td>
							<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
						</tr>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/RecordedPracticeTransforms/1_wrong.json'>1_wrong.json</a></b></td>
							<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
						</tr>
					</table>
				</blockquote>
			</details>
			<!-- Editor Submodule -->
			<details>
				<summary><b>Editor</b></summary>
				<blockquote>
					<div class='directory-path' style='padding: 8px 0; color: #666;'>
						<code><b>⦿ Assets.Editor</b></code>
					<table style='width: 100%; border-collapse: collapse;'>
					<thead>
						<tr style='background-color: #f8f9fa;'>
							<th style='width: 30%; text-align: left; padding: 8px;'>File Name</th>
							<th style='text-align: left; padding: 8px;'>Summary</th>
						</tr>
					</thead>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/Editor/BodyDisplayControllerDrawer.cs'>BodyDisplayControllerDrawer.cs</a></b></td>
							<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
						</tr>
					</table>
				</blockquote>
			</details>
			<!-- Virtual Limb Animation Submodule -->
			<details>
				<summary><b>Virtual Limb Animation</b></summary>
				<blockquote>
					<div class='directory-path' style='padding: 8px 0; color: #666;'>
						<code><b>⦿ Assets.Virtual Limb Animation</b></code>
					<table style='width: 100%; border-collapse: collapse;'>
					<thead>
						<tr style='background-color: #f8f9fa;'>
							<th style='width: 30%; text-align: left; padding: 8px;'>File Name</th>
							<th style='text-align: left; padding: 8px;'>Summary</th>
						</tr>
					</thead>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/Virtual Limb Animation/FeetHit.anim'>FeetHit.anim</a></b></td>
							<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
						</tr>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/Virtual Limb Animation/LeftHandHoldStick.anim'>LeftHandHoldStick.anim</a></b></td>
							<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
						</tr>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/Virtual Limb Animation/RightHandHoldStick.anim'>RightHandHoldStick.anim</a></b></td>
							<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
						</tr>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/Virtual Limb Animation/Right Hand IK.controller'>Right Hand IK.controller</a></b></td>
							<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
						</tr>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/Virtual Limb Animation/Right Leg IK_target.controller'>Right Leg IK_target.controller</a></b></td>
							<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
						</tr>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/Virtual Limb Animation/Left Hand IK.controller'>Left Hand IK.controller</a></b></td>
							<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
						</tr>
					</table>
				</blockquote>
			</details>
			<!-- Metronome Submodule -->
			<details>
				<summary><b>Metronome</b></summary>
				<blockquote>
					<div class='directory-path' style='padding: 8px 0; color: #666;'>
						<code><b>⦿ Assets.Metronome</b></code>
					<table style='width: 100%; border-collapse: collapse;'>
					<thead>
						<tr style='background-color: #f8f9fa;'>
							<th style='width: 30%; text-align: left; padding: 8px;'>File Name</th>
							<th style='text-align: left; padding: 8px;'>Summary</th>
						</tr>
					</thead>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/Metronome/Metronome Note.prefab'>Metronome Note.prefab</a></b></td>
							<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
						</tr>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/Metronome/Metronome.prefab'>Metronome.prefab</a></b></td>
							<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
						</tr>
						<tr style='border-bottom: 1px solid #eee;'>
							<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/Metronome/MetronomeNote.mat'>MetronomeNote.mat</a></b></td>
							<td style='padding: 8px;'>Code>❯ REPLACE-ME</code></td>
						</tr>
					</table>
				</blockquote>
			</details>
		</blockquote>
	</details>
</details>

---

## Getting Started

### Prerequisites

This project requires the following dependencies:

- **Programming Language:** C#
- Unity 2022.3.34f1 or later
- Quest Link PCVR
- Quest 3 HMD or later version that supports passthrough
- Electronic Drum Kit or any MIDI input device, I use Roland Td-1dmk for development

### Installation

Build ARDrum-VirtualHand from the source:

1. **Clone the repository:**

    ```sh
    ❯ git clone https://github.com/wayne0419/ARDrum-VirtualHand
    ```

2. **Open with Unity Engine**

3. **Open Scene ```IK Rig_Controller.unity```**

4. **Make any adjustment that you want to**

### Usage - Record
There are two ways to use the record phase features:

1. Record in the buid application.
2. Record in the Unity editor.

To record in the buid application:

1. **Follow [Usage - Playback Section](#Usage---Playback) step 1~5**

2. **Enter record phase**

	Press number 0 on your pc keyboard (the number 0 on the top, not the number pad) to enter record phase.

3. **Check record status**

	The color of the big sphere in the front shows the record status.
	<table>
	<tr>
	<th>🟢</th>
	<td>Idling.</td>
	</tr>
	<tr>
	<th>🟡</th>
	<td>Ready. <blockquote>After pressing the start record button, the system goes into this state for 4 beats before entering into the recording state.</blockquote></td>
	</tr>
	<tr>
	<th>🔴</th>
	<td>Recording.</td>
	</tr>
	</table>

4. **Record phase control scheme**
	| Functionality | Key Binding |
	|---|---|
	| Start record | Space |
	| Speed up/down record tempo | Mousepad +/- |

5. **Record whatever you want**

	The recording are stored inside ```Assets/RecordedTransforms```.

6. **Switch the recording for playback**
	
	After completing a recording, the latest recording will be automatically assigned as the playback material. To change the recording, in the Unity editor, go to ```Scene Root object -> PlayBackPhaseRunner object-> TransformPlayBacker Object -> TransformPlayBacker component``` and adjust the ```Json File Path``` property.

### Usage - Playback

1. **Build the application**

	Inside Unity Engine, select File -> Build Settings -> Windows, Mac, Linux -> Build

2. **Copy directory ```Assets -> RecordedTransforms``` into the build folder**

	```sh
    The file structure of your build folder should look like this

	<Your build folder>
    ├── Assets
    │    └── RecorderTransforms
	│           └── <at least one recording .json file>
	├── ARDrum-VirtualHand.exe
	└── ...
    ```
3. **Connect HMD with PC through Quest Link**
4. **Execute ```ARDrum-VirtualHand.exe```**
5. **Align physical and virtual drumsets**

	Use Quest controller button B to turn on a white virtual drumset, and align with your own physical drumset, then press button B again to turn it off.

6. **Enter playback phase**

	Press number 1 on your pc keyboard (the number 1 on the top, not the number pad) to enter playback phase.

7. **Playback phase control scheme**
	| Functionality | Key Binding |
	|---|---|
	| Start/Stop playback | Space |
	| Speed up/down tempo | Mousepad +/- |
	| Skip/Unskip a music note | Click on that note on the drum sheet. |
	| Use playback preset | AQWEFRTYJUIO;P[] |

<!-- ### Testing

Ardrum-virtualhand uses the {__test_framework__} test framework. Run the test suite with:

echo 'INSERT-TEST-COMMAND-HERE' -->

<!-- --- -->

<!-- ## Roadmap

- [X] **`Task 1`**: <strike>Implement feature one.</strike>
- [ ] **`Task 2`**: Implement feature two.
- [ ] **`Task 3`**: Implement feature three. -->

---

## Contributing

- **💬 [Join the Discussions](https://github.com/wayne0419/ARDrum-VirtualHand/discussions)**: Share your insights, provide feedback, or ask questions.
- **🐛 [Report Issues](https://github.com/wayne0419/ARDrum-VirtualHand/issues)**: Submit bugs found or log feature requests for the `ARDrum-VirtualHand` project.
- **💡 [Submit Pull Requests](https://github.com/wayne0419/ARDrum-VirtualHand/blob/main/CONTRIBUTING.md)**: Review open PRs, and submit your own PRs.

<details closed>
<summary>Contributing Guidelines</summary>

1. **Fork the Repository**: Start by forking the project repository to your github account.
2. **Clone Locally**: Clone the forked repository to your local machine using a git client.
   ```sh
   git clone https://github.com/wayne0419/ARDrum-VirtualHand
   ```
3. **Create a New Branch**: Always work on a new branch, giving it a descriptive name.
   ```sh
   git checkout -b new-feature-x
   ```
4. **Make Your Changes**: Develop and test your changes locally.
5. **Commit Your Changes**: Commit with a clear message describing your updates.
   ```sh
   git commit -m 'Implemented new feature x.'
   ```
6. **Push to github**: Push the changes to your forked repository.
   ```sh
   git push origin new-feature-x
   ```
7. **Submit a Pull Request**: Create a PR against the original project repository. Clearly describe the changes and their motivations.
8. **Review**: Once your PR is reviewed and approved, it will be merged into the main branch. Congratulations on your contribution!
</details>

<details closed>
<summary>Contributor Graph</summary>
<br>
<p align="left">
   <a href="https://github.com{/wayne0419/ARDrum-VirtualHand/}graphs/contributors">
      <img src="https://contrib.rocks/image?repo=wayne0419/ARDrum-VirtualHand">
   </a>
</p>
</details>

---

## License

Ardrum-virtualhand is protected under the Apache-2.0 License. For more details, refer to the [LICENSE](https://choosealicense.com/licenses/) file.

---

## Acknowledgments

- [Minis: MIDI input extension for Unity Input System](https://github.com/keijiro/Minis) by Keijiro

<div align="right">

[![][back-to-top]](#top)

</div>


[back-to-top]: https://img.shields.io/badge/-BACK_TO_TOP-151515?style=flat-square


---
