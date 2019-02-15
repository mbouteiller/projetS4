using UnityEngine;
using System.Collections;

//<summary>
//Game object, that creates maze and instantiates it in scene
//</summary>
public class Spawner : MonoBehaviour {

	public bool FullRandom = false;
	public int RandomSeed = 12345;
	public GameObject Floor = null;
	public GameObject Wall = null;
	public GameObject Check = null;
	public GameObject RedBase = null;
	public GameObject BlueBase = null;
	public GameObject Object1 = null;
	public GameObject Object2 = null;
	public GameObject Object3 = null;
	public int Rows = 8;
	public int Columns = 8;
	public float CellWidth = 5;
	public float CellHeight = 5;
	public bool AddGaps = true;

	private BaseLogic MazeGenerator = null;

	void Start () {
		if (!FullRandom) {
			Random.seed = RandomSeed;
		}
		MazeGenerator = new DivisionMaze (Rows, Columns);

		MazeGenerator.GenerateMaze ();
		for (int row = 0; row < Rows; row++) {
			for(int column = 0; column < Columns; column++){
				float x = column*(CellWidth+(AddGaps?.2f:0));
				float z = row*(CellHeight+(AddGaps?.2f:0));
				MazeCell cell = MazeGenerator.GetMazeCell(row,column);
				if((row > 1 || column > 1) && (row < Rows - 2 || column < Columns - 2)) {
					GameObject tmp;
					tmp = Instantiate(Floor,new Vector3(x,0,z), Quaternion.Euler(0,0,0)) as GameObject;
					tmp.transform.parent = transform;
					if(cell.WallRight){
						tmp = Instantiate(Wall,new Vector3(x+CellWidth/2,0,z)+Wall.transform.position,Quaternion.Euler(0,90,0)) as GameObject;// right
						tmp.transform.parent = transform;
					}
					if(cell.WallFront){
						tmp = Instantiate(Wall,new Vector3(x,0,z+CellHeight/2)+Wall.transform.position,Quaternion.Euler(0,0,0)) as GameObject;// front
						tmp.transform.parent = transform;
					}
					if(cell.WallLeft){
						tmp = Instantiate(Wall,new Vector3(x-CellWidth/2,0,z)+Wall.transform.position,Quaternion.Euler(0,270,0)) as GameObject;// left
						tmp.transform.parent = transform;
					}
					if(cell.WallBack){
						tmp = Instantiate(Wall,new Vector3(x,0,z-CellHeight/2)+Wall.transform.position,Quaternion.Euler(0,180,0)) as GameObject;// back
						tmp.transform.parent = transform;
					}
					if(Random.Range(0, 20) == 7) {
						int test = Random.Range(0, 3);
						if(test == 0) {
							tmp = Instantiate(Object1,new Vector3(x,1,z)+Wall.transform.position,Quaternion.Euler(0,180,0)) as GameObject;
							tmp.transform.parent = transform;
						}
						else if (test == 1) {
								tmp = Instantiate(Object2,new Vector3(x,1,z)+Wall.transform.position,Quaternion.Euler(0,180,0)) as GameObject;
								tmp.transform.parent = transform;
						}
						else {
								tmp = Instantiate(Object3,new Vector3(x,1,z)+Wall.transform.position,Quaternion.Euler(0,180,0)) as GameObject;
								tmp.transform.parent = transform;
						}
					}
				}
				else {
					GameObject tmp;
					if(row == 1 && column == 1) {
						tmp = Instantiate(RedBase,new Vector3(x,0,z), Quaternion.Euler(0,0,0)) as GameObject;
					}
					if(row == Rows - 2 && column == Columns - 2) {
						tmp = Instantiate(BlueBase,new Vector3(x,0,z), Quaternion.Euler(0,0,0)) as GameObject;
					}
				}
			}
		}
			

		if(Check != null){
			for (int row = 0; row < Rows+1; row++) {
				for (int column = 0; column < Columns+1; column++) {
					float x = column*(CellWidth+(AddGaps?.2f:0));
					float z = row*(CellHeight+(AddGaps?.2f:0));
					GameObject tmp = Instantiate(Check,new Vector3(x-CellWidth/2,0,z-CellHeight/2),Quaternion.identity) as GameObject;
					tmp.transform.parent = transform;
				}
			}
		}
	}
}
