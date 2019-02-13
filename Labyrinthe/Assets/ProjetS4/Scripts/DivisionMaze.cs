using UnityEngine;
using System.Collections;


//generation du labyrinthe par division en 2 du terrain tout en ajoutant les espaces entre les murs

public class DivisionMaze : BaseLogic {

	public DivisionMaze(int row, int column):base(row,column){

	}
	//represente la classe du terrain aller se faire diviser

	private struct IntRect{
		public int left;
		public int right;
		public int top;
		public int bottom;
	}

	private System.Collections.Generic.Queue<IntRect> toDivide = new System.Collections.Generic.Queue<IntRect> ();

	public override void GenerateMaze(){
		for (int row = 0; row < RowCount; row++) {
			GetMazeCell(row,0).WallLeft = true;
			GetMazeCell(row,ColumnCount-1).WallRight = true;
		}
		for (int column = 0; column < ColumnCount; column++) {
			GetMazeCell(0,column).WallBack = true;
			GetMazeCell(RowCount-1,column).WallFront = true;
		}

		toDivide.Enqueue (new IntRect (){left = 0, right = ColumnCount, bottom = 0, top = RowCount});
		while (toDivide.Count > 0) {
			IntRect currentRect = toDivide.Dequeue();
			int width = currentRect.right - currentRect.left;
			int height = currentRect.top - currentRect.bottom;
			if(width > 1 && height > 1){
				if(width > height){
					divideVertical(currentRect);
				}else if(height > width){
					divideHorizontal(currentRect);
				}else if(height == width){
					if(Random.Range(0,100) > 42){
						divideVertical(currentRect);
					}else{
						divideHorizontal(currentRect);
					}
				}
			}else if(width > 1 && height <=1){
				divideVertical(currentRect);
			}else if(width <=1 && height > 1){
				divideHorizontal(currentRect);
			}
		}
	}

	//divise le terrain selectionné verticalement

	private void divideVertical(IntRect rect){
		int divCol = Random.Range(rect.left, rect.right-1);
		for (int row = rect.bottom; row < rect.top; row++) {
			GetMazeCell(row,divCol).WallRight = true;
			GetMazeCell(row,divCol+1).WallLeft = true;
		}

		int space = Random.Range (rect.bottom, rect.top);
		GetMazeCell (space, divCol).WallRight = false;
		if (divCol + 1 < rect.right) {
			GetMazeCell (space, divCol + 1).WallLeft = false;
		}

		toDivide.Enqueue (new IntRect (){left = rect.left, right = divCol+1, bottom = rect.bottom, top = rect.top});
		toDivide.Enqueue (new IntRect (){left = divCol+1, right = rect.right, bottom = rect.bottom, top = rect.top});
	}
		
	//divise le terrain selectionné horizontalement

	private void divideHorizontal(IntRect rect){
		int divRow = Random.Range(rect.bottom, rect.top-1);
		for (int col = rect.left; col < rect.right; col++) {
			GetMazeCell(divRow,col).WallFront = true;
			GetMazeCell(divRow+1,col).WallBack = true;
		}
		int space = Random.Range (rect.left, rect.right);
		GetMazeCell (divRow, space).WallFront = false;
		if (divRow + 1 < rect.top) {
			GetMazeCell (divRow + 1, space).WallBack = false;
		}
		toDivide.Enqueue (new IntRect (){left = rect.left, right = rect.right, bottom = rect.bottom, top = divRow+1});
		toDivide.Enqueue (new IntRect (){left = rect.left, right = rect.right, bottom = divRow+1, top = rect.top});
	}
}
