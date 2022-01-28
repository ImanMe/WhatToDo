
import { Component, OnInit } from '@angular/core';
import { CreateItem, UpdateItem } from '../models/createItem';
import { ToDo } from '../models/toDo';
import { ToDoService } from '../to-do.service';

@Component({
  selector: 'app-root',
  templateUrl: './home.component.html'
})
export class HomeComponent implements OnInit {
  toDoList = 'TODO ITEMS';
  completedList = 'COMPLETED ITEMS';
  allItems: ToDo[] = [];
  completedTasks: ToDo[] = [];
  inCompletedTasks: ToDo[] = [];
  inputValue: string = "";
  createItemObj: CreateItem = new CreateItem("");

  constructor(private toDoService: ToDoService) {
  }

  ngOnInit(): void {
    this.getAd();
  }

  getAd = () => {
    this.toDoService.getItems().subscribe(response => {
      this.allItems = response,
        this.completedTasks = this.allItems.filter(x => x.isCompleted),
        this.inCompletedTasks = this.allItems.filter(x => !x.isCompleted);
    });
  }

  create = () => {
    this.toDoService.createItem(this.createItemObj).subscribe(result => {
      this.getAd();
    }), console.error();
  }

  onDelete = (id: number) => {
    this.toDoService.deleteItem(id).subscribe(result => {
      this.getAd();
    }), console.error();
  }

  onComplete = (updateItem: UpdateItem) => {
    this.toDoService.updateItem(updateItem).subscribe(result => {
      this.getAd();
    }), console.error();
  }
}


