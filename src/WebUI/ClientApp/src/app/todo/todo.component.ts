import { Component,OnInit } from '@angular/core';
import { PostsClient,PostDto
} from '../web-api-client';

@Component({
  selector: 'app-todo-component',
  templateUrl: './todo.component.html',
  styleUrls: ['./todo.component.scss']
})
export class TodoComponent implements OnInit {
  debug = false;
  posts: PostDto[];

  constructor(
    private postClient: PostsClient,
  ) {}

  ngOnInit(): void {
    this.postClient.get().subscribe(
      resut=>{
        this.posts=resut.posts
        },
      error => console.error(error)  
    );
  }
}