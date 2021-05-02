using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Yarn;
using Yarn.Unity;
using UnityEngine.UI;
using System.Text;
using Thuleanx.Optimization;
using Thuleanx.Dialogue;
using Thuleanx.Math;
using UnityEngine.InputSystem;

namespace Yarn_Thuleanx {
	public class Thuleanx_Dialogue_UI : Yarn.Unity.DialogueUIBehaviour {

		[Tooltip("How quickly to show the text, in seconds per character")]
		public float SecondsPerCharacter = 0.025f;
		public List<Button> optionButtons;

		public float WaitAfterLine = 2f;
		public float WaitAfterLinePerCharacter = .025f;

		public BubblePool DialoguePool;
		public Canvas DialogueCanvas;

		private bool userRequestedNextLine = false;
		private System.Action<int> currentOptionSelectionHandler;
		private bool waitingForOptionSelection = false;

		public UnityEvent onDialogueStart;
		public UnityEvent onDialogueEnd;
		public DialogueRunner.StringUnityEvent onLineStart;
		public UnityEvent onLineFinishDisplaying;
		public DialogueRunner.StringUnityEvent onLineUpdate;
		public UnityEvent onLineEnd;
		public UnityEvent onOptionsStart;
		public UnityEvent onOptionsEnd;
		public DialogueRunner.StringUnityEvent onCommand;

		internal void Awake() {
			foreach (var button in optionButtons)
				button.gameObject.SetActive(false);
		}

		public override Dialogue.HandlerExecutionType RunLine(Line line,
			ILineLocalisationProvider localisationProvider, Action onLineComplete) {

			StartCoroutine(DoRunLine(line, localisationProvider, onLineComplete));
			return Dialogue.HandlerExecutionType.PauseExecution;
		}

		private IEnumerator DoRunLine(Yarn.Line line, ILineLocalisationProvider localisationProvider, System.Action onComplete) {
			userRequestedNextLine = false;

			string unparsedText = localisationProvider.GetLocalisedTextForLine(line);

			if (unparsedText == null) {
				Debug.LogWarning($"Line {line.ID} doesn't have any localised unparsedText.");
				unparsedText = line.ID;
			}

			string speakerName = "Player";
			string text = unparsedText;
			if (unparsedText != null && unparsedText.IndexOf(':') != -1) {
				speakerName = unparsedText.Substring(0, unparsedText.IndexOf(':'));
				text = unparsedText.Substring(unparsedText.IndexOf(':') + 1).TrimStart();
			}

			onLineStart?.Invoke(speakerName);

			Speaker speaker = Speaker.GetSpeaker(speakerName);

			GameObject dialogueBubble = DialoguePool.Borrow(gameObject.scene);
			dialogueBubble.transform.SetParent(DialogueCanvas.transform);
			DialogueText dialogueText = dialogueBubble.GetComponentInChildren<DialogueText>();
			dialogueText.SetSpeaker(speaker);
			dialogueText.SetText("");

			if (SecondsPerCharacter > 0.0f) {
				// Display the line one character at a time
				var stringBuilder = new StringBuilder();

				foreach (char c in text) {
					stringBuilder.Append(c);
					dialogueText?.SetText(stringBuilder.ToString());
					onLineUpdate?.Invoke(stringBuilder.ToString());
					if (userRequestedNextLine) {
						// We've requested a skip of the entire line.
						// Display all of the text immediately.
						dialogueText?.SetText(text);
						onLineUpdate?.Invoke(text);
						break;
					}
					yield return new WaitForSeconds(SecondsPerCharacter);
				}
			} else {
				// Display the entire line immediately if textSpeed <= 0
				dialogueText?.SetText(text);
				onLineUpdate?.Invoke(text);
			}


			userRequestedNextLine = false;

			onLineFinishDisplaying?.Invoke();

			Timer Wait = new Timer(WaitAfterLine + WaitAfterLinePerCharacter * text.Length);
			Wait.Start();

			while (userRequestedNextLine == false && Wait) {
				yield return null;
			}

			// Avoid skipping lines if textSpeed == 0
			yield return new WaitForEndOfFrame();

			dialogueText?.Disable();
			// Hide the text and prompt
			onLineEnd?.Invoke();

			onComplete();
		}

		public override void RunOptions(OptionSet optionSet,
			ILineLocalisationProvider localisationProvider, Action<int> onOptionSelected) {
			StartCoroutine(DoRunOptions(optionSet, localisationProvider, onOptionSelected));
		}

		private IEnumerator DoRunOptions(Yarn.OptionSet optionsCollection,
			ILineLocalisationProvider localisationProvider, System.Action<int> selectOption) {

			if (optionsCollection.Options.Length > optionButtons.Count) {
				Debug.LogWarning("There are more options to present than there are" +
								 "buttons to present them in. This will cause problems.");
			}

			int i = 0;

			waitingForOptionSelection = true;

			currentOptionSelectionHandler = selectOption;

			foreach (var optionString in optionsCollection.Options) {
				optionButtons[i].gameObject.SetActive(true);

				optionButtons[i].onClick.RemoveAllListeners();
				optionButtons[i].onClick.AddListener(() => SelectOption(optionString.ID));

				var optionText = localisationProvider.GetLocalisedTextForLine(optionString.Line);

				if (optionText == null) {
					Debug.LogWarning($"Option {optionString.Line.ID} doesn't have any localised text");
					optionText = optionString.Line.ID;
				}

				var unityText = optionButtons[i].GetComponentInChildren<Text>();
				if (unityText != null) {
					unityText.text = optionText;
				}

				var textMeshProText = optionButtons[i].GetComponentInChildren<TMPro.TMP_Text>();
				if (textMeshProText != null) {
					textMeshProText.text = optionText;
					// refresh layout
					LayoutRebuilder.ForceRebuildLayoutImmediate(optionButtons[i].GetComponent<RectTransform>());
				}

				i++;
			}

			onOptionsStart?.Invoke();

			// Wait until the chooser has been used and then removed 
			while (waitingForOptionSelection) {
				yield return null;
			}

			// Hide all the buttons
			foreach (var button in optionButtons) {
				button.gameObject.SetActive(false);
			}

			onOptionsEnd?.Invoke();
		}

		public override void DialogueStarted() {
			onDialogueStart?.Invoke();
		}

		public override void DialogueComplete() {
			onDialogueEnd?.Invoke();
		}

		public void MarkLineComplete() {
			userRequestedNextLine = true;
		}

		public void SelectOption(int optionID) {
			if (waitingForOptionSelection == false) {
				Debug.LogWarning("An option was selected, but the dialogue UI was not expecting it.");
				return;
			}
			waitingForOptionSelection = false;
			currentOptionSelectionHandler?.Invoke(optionID);
		}

		public override Dialogue.HandlerExecutionType RunCommand(Command command, Action onCommandComplete) {
			onCommand?.Invoke(command.Text);

			// Signal to the DialogueRunner that it should continue
			// executing. (This implementation of RunCommand always signals
			// that execution should continue, and never calls
			// onCommandComplete.)
			return Dialogue.HandlerExecutionType.ContinueExecution;
		}

		public void OnSkipInput(InputAction.CallbackContext context) {
			if (context.started)
				MarkLineComplete();
		}
	}
}