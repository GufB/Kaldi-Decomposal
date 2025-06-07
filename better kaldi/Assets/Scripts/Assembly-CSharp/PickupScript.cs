using UnityEngine;

public class PickupScript : MonoBehaviour
{
	public GameControllerScript gc;

	public Transform player;

	public int ID;

	private void Update()
	{
		if (Input.GetMouseButtonDown(0) && Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out var hitInfo) && ((hitInfo.transform.gameObject == base.transform.gameObject) & (Vector3.Distance(player.position, base.transform.position) < 15f)))
		{
			if ((gc.item[0] == 0) | (gc.item[1] == 0) | (gc.item[2] == 0) | (gc.item[3] == 0) | (gc.item[4] == 0))
			{
				hitInfo.transform.gameObject.SetActive(value: false);
				gc.CollectItem(ID);
				return;
			}
			int iD = ID;
			ID = gc.item[gc.itemSelected];
			Texture texture = gc.itemTextures[ID];
			Sprite sprite = Sprite.Create((Texture2D)texture, new Rect(0f, 0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), texture.width);
			GetComponentInChildren<SpriteRenderer>().transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
			GetComponentInChildren<SpriteRenderer>().sprite = sprite;
			gc.CollectItem(iD);
		}
	}
}
