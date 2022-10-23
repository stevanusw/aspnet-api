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

  constructor(private http: HttpClient) {
  }

  ngOnInit(): void {
    this.http.get<Todo[]>(environment.baseUrl + 'api/v1/todos').subscribe(result => {
      this.todos = result;
    }, error => console.error(error));
  }
}

interface Todo {
  id: number;
  name: string;
  isCompleted: boolean;
  createDateUtc: string;
}

