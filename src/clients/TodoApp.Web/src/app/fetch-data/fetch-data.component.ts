import { Component, OnInit } from '@angular/core';
import { HttpClient } from "@angular/common/http";

import { environment } from "../../environments/environment";

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html',
  styleUrls: ['./fetch-data.component.css']
})
export class FetchDataComponent implements OnInit {
  public todos?: Todo[];

  constructor(http: HttpClient) {
    http.get<Todo[]>(environment.baseUrl + 'api/v1/todos').subscribe(result => {
      this.todos = result;
    }, error => console.error(error));
  }

  ngOnInit(): void {
  }
}

interface Todo {
  id: number;
  name: string;
  isCompleted: boolean;
  createDateUtc: string;
}

