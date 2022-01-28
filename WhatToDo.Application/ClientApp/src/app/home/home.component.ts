
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
  isDuplicate: boolean = false;

  constructor(private toDoService: ToDoService) {
  }

  ngOnInit(): void {
    this.getAds();
  }

  getAds = () => {
    this.isDuplicate = false;
    this.toDoService.getItems().subscribe(response => {
      this.allItems = response,
        this.completedTasks = this.allItems.filter(x => x.isCompleted),
        this.inCompletedTasks = this.allItems.filter(x => !x.isCompleted);
    });
  }

  create = () => {  
    if (this.isValidDescription()) {
      console.log('comee', this.createItemObj.description);
      
      this.toDoService.createItem(this.createItemObj).subscribe(result => {
        this.createItemObj.description = '',
          this.getAds();
      }), console.error();
    }
  }

  onDelete = (id: number) => {
    this.toDoService.deleteItem(id).subscribe(result => {
      this.getAds();
    }), console.error();
  }

  onComplete = (updateItem: UpdateItem) => {
    this.toDoService.updateItem(updateItem).subscribe(result => {
      this.getAds();
    }), console.error();
  }

  isValidDescription = () => {
    this.isDuplicate = false;
    this.isDescriptionDuplicate();
    if(!this.isDuplicate && this.createItemObj.description && this.createItemObj.description.trim()) return true;
    return false;
  }

  isDescriptionDuplicate = (): void => {
    const result = this.inCompletedTasks
    .some(el => el.description.toUpperCase() === this.createItemObj.description.toUpperCase());

    if (result) this.isDuplicate = true;
  }
}

