using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200001C RID: 28
public class MenuController : MonoBehaviour
{
	// Token: 0x06000063 RID: 99 RVA: 0x00003D32 File Offset: 0x00002132
	private void Start()
	{
	}

	// Token: 0x06000064 RID: 100 RVA: 0x00003D45 File Offset: 0x00002145
	public void OnEnable()
	{
		this.uc.firstButton = this.firstButton;
		this.uc.dummyButtonPC = this.dummyButtonPC;
		this.uc.dummyButtonElse = this.dummyButtonElse;
		this.uc.SwitchMenu();
	}

	// Token: 0x06000065 RID: 101 RVA: 0x00003D85 File Offset: 0x00002185
	private void Update()
{
	if (Input.GetButtonDown("Cancel") && this.back != null)
	{
		this.back.SetActive(true);
		base.gameObject.SetActive(false);
	}

	int branull = PlayerPrefs.GetInt("Branull", 1);

	if (branull == 1)
	{
		titleMain.SetActive(false);
		titleNull.SetActive(true);
		object11.SetActive(true);
		object2.SetActive(false);
		object3.SetActive(false);
		object4.SetActive(false);
		pussyOut.SetActive(true);
		pussyIn.SetActive(false);
	}
	else if (branull == 0)
	{
		titleMain.SetActive(true);
		titleNull.SetActive(false);
		object11.SetActive(false);
		object2.SetActive(true); 
		object3.SetActive(true);
		object4.SetActive(true);
		pussyOut.SetActive(false);
		pussyIn.SetActive(true);
	}
}


	// Token: 0x04000082 RID: 130
	public UIController uc;

	// Token: 0x04000083 RID: 131
	public Selectable firstButton;

	// Token: 0x04000084 RID: 132
	public Selectable dummyButtonPC;

	// Token: 0x04000085 RID: 133
	public Selectable dummyButtonElse;

	// Token: 0x04000086 RID: 134
	public GameObject back;
	public GameObject titleMain;
	public GameObject titleNull;
	public GameObject object11;
	public GameObject object2;
	public GameObject object3;
	public GameObject object4;
	public GameObject pussyOut;
	public GameObject pussyIn;

}
