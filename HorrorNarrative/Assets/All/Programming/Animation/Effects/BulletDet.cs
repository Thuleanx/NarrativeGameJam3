using UnityEngine;
using Thuleanx.Math;

namespace Thuleanx.Animation {
	public class BulletDet : MonoBehaviour {
		[SerializeField] Sprite Bullet, BulletFlash, BulletMuzzleFlash;
		[SerializeField] float flashTime = .2f, switchTime = .125f;

		SpriteRenderer Sprite;
		Timer flashing;
		Timer sustainFrame;

		void Awake() {
			Sprite = GetComponent<SpriteRenderer>();
		}

		void OnEnable() {
			flashing = new Timer(flashTime);
			flashing.Start();
		}

		void Update() {
			if (flashing) Sprite.sprite = BulletMuzzleFlash;
			else if (!flashing && !sustainFrame) {
				sustainFrame = new Timer(switchTime);
				sustainFrame.Start();
				if (Sprite.sprite == Bullet) Sprite.sprite = BulletFlash;
				else Sprite.sprite = Bullet;
			}
		}
	}
}