<div id="top">

<!-- HEADER STYLE: CLASSIC -->
<div align="center">

<img src="readme_materials/MRDrum logo transparent background.png" width="30%" style="position: relative; top: 0; right: 0;" alt="Project Logo"/>

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

ARDrum-VirtualHand is the implementation of the micro-progressed AR self-learning drumming system presented within a full paper in
ACM CHI 2025 (https://dl.acm.org/doi/10.1145/3706598.3714156).

The original paper introduced the idea of utilizing the concept of micro-progression to break through the long-standing challenge of using AR to learn/train complex (temporally and physically) skills, drumming, as an example.

As the research and evaluation are detailed in the paper, this document focuses on the system itself. The following sections will cover its features, project structure, and a brief overview of the codebase.

Let’s get started!

[![Video Teaser Thumbnail](readme_materials/01_Teaser_video.png) Teaser Video](https://www.youtube.com/watch?v=DkBCzBZu6SM)

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
ARDrum-VirtualHand/
└── Assets
    ├── BodyDisplayController.cs
    ├── Character/
    ├── CorrectRateCalculator.cs
    ├── CorrectRateDisplayer.cs
    ├── DisplayControl.cs
    ├── Drum Sheet/
    ├── Drum Video/
    ├── DrumHitIndicator.cs
    ├── DrumKit.cs
    ├── DrumNote.cs
    ├── DrumSheet.cs
    ├── DrumSheetCursor.cs
    ├── DrumSheetPlayer.cs
    ├── DrumStickPath/
    ├── DrumType.cs
    ├── DrumVideoPlayer.cs
    ├── Drum_SFX/
    ├── Editor
    │   └── BodyDisplayControllerDrawer.cs
    ├── Foot Magic Mirror/
    ├── HandFollowController.cs
    ├── HandMovementFeedback.cs
    ├── HandMovementPathRenderer.cs
    ├── Level Panel Button/
    ├── LevelController.cs
    ├── LevelManagementAnnouncement.cs
    ├── LevelManager.cs
    ├── Metronome/
    ├── Metronome.cs
    ├── MetronomeNote.cs
    ├── PhaseManager.cs
    ├── PhaseRunner.cs
    ├── PlayBackPhaseRunner.cs
    ├── PlayBackStatusVisualizer.cs
    ├── PracticeRecordPhaseRunner.cs
    ├── PracticeRecorder.cs
    ├── RealTimeInputLogSaver.cs
    ├── RealTimeInputTracker.cs
    ├── RecordPhaseRunner.cs
    ├── RecordStatusVisualizer.cs
    ├── RecordedPracticeTransforms/
    ├── RecordedTransforms/
    ├── ReviewManager.cs
    ├── ReviewPhaseRunner.cs
    ├── ReviewSheetDrawer.cs
    ├── Scenes
    │   ├── IK Rig_Controller.unity
    │   └── IK Rig_Hand.unity
    ├── SetDrumNoteSkipStateButton.cs
    ├── SetHitDrumCorrectMode.cs
    ├── StudyControl.cs
    ├── TransformPlayBacker.cs
    ├── TransformRecorder.cs
    ├── Virtual Drum/
    ├── Virtual Drum Sheet/
    ├── Virtual Limb Animation/
    ├── Virtual Video Panel/
    └── VirtualDrumController.cs
```

### Project Index

<details open>
	<summary><b><code>ARDRUM-VIRTUALHAND/</code></b></summary>
	<!-- Assets Submodule -->
	<details closed>
		<summary><b>Assets</b></summary>
		<blockquote>
			<div class='directory-path' style='padding: 8px 0; color: #666;'>
				<code><b>ARDRUM-VIRTUALHAND/Assets</b></code>
			<table style='width: 100%; border-collapse: collapse;'>
			<thead>
				<tr style='background-color: #f8f9fa;'>
					<th style='width: 30%; text-align: left; padding: 8px;'>File Name</th>
					<th style='text-align: left; padding: 8px;'>Summary</th>
				</tr>
			</thead>
				<tr style='border-bottom: 1px solid #eee;'>
					<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/SetDrumNoteSkipStateButton.cs'>SetDrumNoteSkipStateButton.cs</a></b></td>
					<td style='padding: 8px;'>Enable dynamic control over which drum notes or limb movements should be skipped during playback by linking UI buttons or input actions to specific selection modes. <code>SetDrumNoteSkipStateButton.cs</code> maps user interactions to skip-state toggling for individual limbs, drum types, beat ranges, or predefined practice configurations. It plays a key role in customizing the playback experience through the <code>TransformPlayBacker</code>'s <code>DrumSheet</code>, facilitating targeted practice and error isolation in the self-learning drumming system.</td>
				</tr>
				<tr style='border-bottom: 1px solid #eee;'>
					<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/LevelManagementAnnouncement.cs'>LevelManagementAnnouncement.cs</a></b></td>
					<td style='padding: 8px;'>Displays a flashing on-screen announcement when a level is passed, enhancing player feedback and reinforcing progress. It subscribes to <code>LevelManager</code>’s <code>OnLevelPassed</code> event and temporarily toggles the visibility of a <code>TextMeshProUGUI</code> element to create a blinking visual cue. <code>LevelManagementAnnouncement.cs</code> serves as an immediate reward mechanism and complements the level progression system managed by <code>LevelManager</code>.
</td>
				</tr>
				<tr style='border-bottom: 1px solid #eee;'>
					<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/HandMovementPathRenderer.cs'>HandMovementPathRenderer.cs</a></b></td>
					<td style='padding: 8px;'>Visualizes and highlights recent left and right hand trajectories during playback by rendering motion paths with <code>LineRenderer</code> components. Anchored to the user’s virtual drumstick tips, the <code>HandMovementPathRenderer</code> system samples motion data before and after each detected drum hit to reconstruct motion arcs. It also identifies and stores key points such as start, end, and highest positions, enabling synchronized visual feedback when paired with <code>HandMovementFeedback</code>. This component enhances user understanding of spatial drumming accuracy and form.
</td>
				</tr>
				<tr style='border-bottom: 1px solid #eee;'>
					<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/MetronomeNote.cs'>MetronomeNote.cs</a></b></td>
					<td style='padding: 8px;'>Visualizes metronome beats during playback by toggling visual states of note indicators in sync with rhythm cues. The <code>MetronomeNote</code> class, used by <code>Metronome</code>, changes color between <code>onColor</code> and <code>highlightColor</code> to signal active beats, helping learners anticipate timing. As part of the broader beat guidance system, <code>MetronomeNote.cs</code> supports rhythm accuracy through clear, time-aligned visual feedback.
</td>
				</tr>
				<tr style='border-bottom: 1px solid #eee;'>
					<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/ReviewManager.cs'>ReviewManager.cs</a></b></td>
					<td style='padding: 8px;'>Enable detailed comparison between user and target drum performances by extracting and aligning hit data from recorded transform files. <code>ReviewManager</code> loads user and target recordings, adjusts their timestamps to a common BPM, and separates each drum’s hit events into categorized lists. These lists support downstream visualization and accuracy assessment, making <code>ReviewManager.cs</code> central to performance review and error analysis in the Review phase.
</td>
				</tr>
				<tr style='border-bottom: 1px solid #eee;'>
					<td style='padding: 8px;'><b><a href='https://github.com/wayne0419/ARDrum-VirtualHand/blob/master/Assets/TransformRecorder.cs'>TransformRecorder.cs</a></b></td>
					<td style='padding: 8px;'>Capture and store synchronized spatial and input data for virtual limb drumming practice.The <code>TransformRecorder</code> component in <code>TransformRecorder.cs</code> logs position, rotation, and drum hit data for three tracked limbs in sync with a metronome’s BPM. It enables configurable delays, integrates footstep animations, and outputs the results as JSON. Closely tied to <code>TransformPlayBacker</code> and input tracking, it is essential for generating structured training datasets and enabling performance playback.
</td>
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
			<!-- Editor Submodule -->
			<details open>
				<summary><b>Editor</b></summary>
				<blockquote>
					<div class='directory-path' style='padding: 8px 0; color: #666;'>
						<code><b>ARDRUM-VIRTUALHAND/Assets/Editor</b></code>
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

<details open>
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
