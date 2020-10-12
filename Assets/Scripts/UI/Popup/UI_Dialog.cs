using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Dialog : UI_Popup
{
	enum Texts
	{
		DialogText,
	}

	enum Images
	{
		ConfirmButton,
		CancelButton,
	}
	int _id;
	int _index;
	int _maxIndex;
	bool _isQuest = false;

	int questTalkIndex;

	public override void Init()
	{
		base.Init();

		Bind<Text>(typeof(Texts));
		Bind<Image>(typeof(Images));

		Get<Image>((int)Images.ConfirmButton).gameObject.BindEvent(OnButtonClicked_Confirm);
		Get<Image>((int)Images.CancelButton).gameObject.BindEvent(OnButtonClicked_Close);
		Get<Image>((int)Images.CancelButton).gameObject.SetActive(false);
	}

	private void OnEnable()
	{
		if (!_isQuest)
		{
			_id = 1000;
			questTalkIndex = Managers.Quest.GetQuestTalkIndex(_id);
		}
		if (Managers.Quest.IsNotEndQuest(_id) || !_isQuest)
		{
			_maxIndex = Managers.Talk.GetTalkLength(_id + questTalkIndex);
			string talk = Managers.Talk.GetTalk(_id + questTalkIndex, 0);
			Get<Text>((int)Texts.DialogText).text = talk;
			Get<Image>((int)Images.CancelButton).gameObject.SetActive(false);
			_isQuest = true;
			Managers.Quest.CheckQuest(_id);
		}
		else
		{
			Debug.Log("끝!");
			string talk = Managers.Talk.GetTalk(_id, 1);
			Get<Text>((int)Texts.DialogText).text = talk;
			Get<Image>((int)Images.CancelButton).gameObject.SetActive(false);
		}		
	}

	public void SetId(int id)
	{
		_id = id;
		_index = 0;
	}

	public void OnButtonClicked_Confirm(PointerEventData data)
	{
		_index++;
		if (_maxIndex > _index)
		{
			string talk = Managers.Talk.GetTalk(_id + questTalkIndex, _index);
			if (talk != null)
			{
				Get<Text>((int)Texts.DialogText).text = talk;
			}
			if (_maxIndex - 1 == _index) Get<Image>((int)Images.CancelButton).gameObject.SetActive(true);
		}
		else
		{
			//Managers.Quest.CheckQuest(_id);
			gameObject.SetActive(false);
		}
	}

	public void OnButtonClicked_Close(PointerEventData data)
	{
		gameObject.SetActive(false);
		//ClosePopupUI();
	}
}
