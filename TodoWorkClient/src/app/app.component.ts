import { CdkDragDrop, moveItemInArray, transferArrayItem } from '@angular/cdk/drag-drop';
import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  todos: TodoModel[] = [];
  todo: TodoModel[] = [];
  done: TodoModel[] = [];
  addTodo: string = "";
  updateTodo: string =  "";
  isAdded: boolean = true;
  isUpdated: boolean = false;
  request: TodoModel = new TodoModel();
  alertMessage: string = "";
  todoName: string = "Update Todo";

  constructor(
    private http: HttpClient
  ){
    this.getAll();
  }

  cancel(){
    this.isUpdated = false;
  }
  cancelAdd(){
    this.addTodo = "";
  }

  AddATodoToDatabase(){
    this.request.id = 0;
    this.request.work = this.addTodo;
    this.http.post<TodoModel[]>(`http://localhost:5157/api/Todos/AddATodo`, this.request)
    .subscribe(res=>{
      this.todos = res;
      this.alertMessage = "A todo succesfully added.";
      this.getAll();
      this.addTodo = "";
    })
  }

  deleteATodoFromDatabase(id:number){
    this.request.id = id;
    this.http.get<TodoModel[]>(`http://localhost:5157/api/Todos/RemoveATodo/${id}`)
    .subscribe(res=>{
      this.todos = res;
      this.alertMessage = "A todo removed successfully";
      this.getAll();
    });
  }

  updateATodoFromDatabase(){
    this.request.work = this.updateTodo;
    this.http.post<TodoModel[]>(`http://localhost:5157/api/Todos/UpdateATodo`,this.request)
    .subscribe(res=>{
      this.todos=res;
      this.updateTodo = "";
      this.alertMessage = "A todo removed successfully";
      this.getAll();
    })
  }

  getATodo(item: TodoModel){
    this.updateTodo = item.work;
    this.request.id = item.id;
    this.request.isCompleted = item.isCompleted;
    this.isUpdated = true;
  }

  getAll(){
    this.http.get<TodoModel[]>("http://localhost:5157/api/Todos/GetAll")
    .subscribe(res=>{
      this.todos = res;
      this.splitTodosToTodoAndDone();
    })
  }

  splitTodosToTodoAndDone(){
    this.todo = [];
    this.done = [];
    for(let t of this.todos){
      if(t.isCompleted) this.done.push(t);
      else this.todo.push(t);
    }
  }

  changeCompleted(id: any){
    console.log(id);
    this.http.get<TodoModel[]>(`http://localhost:5157/api/Todos/ChangeCompleted/${id}`)
    .subscribe(res=>{
      this.todos = res;
      this.splitTodosToTodoAndDone();
    })
  }

  drop1(event: CdkDragDrop<TodoModel[]>) {
    if (event.previousContainer === event.container) {
      moveItemInArray(event.container.data, event.previousIndex, event.currentIndex);
    } else {
      const id = this.done[event.previousIndex].id;      
      this.changeCompleted(id);
    }
  }  

  drop2(event: CdkDragDrop<TodoModel[]>) {
    if (event.previousContainer === event.container) {
      moveItemInArray(event.container.data, event.previousIndex, event.currentIndex);
    } else {
      const id = this.todo[event.previousIndex].id;
      this.changeCompleted(id);
    }
  }  
}

export class TodoModel{
  id: number = 0;
  work: string = "";
  isCompleted: boolean = false;
}
