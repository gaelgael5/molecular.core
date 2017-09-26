import { Component, Input } from '@angular/core';
import { TreeModel } from 'angular-tree-component';
import { ITreeNode } from 'angular-tree-component/dist/defs/api';
import { INode } from '../../Models/Trees/INode';

@Component({
  selector: 'app-filter',
  template: `
    <div>
    <h2>Filter</h2>
    <!--  <input #filter (keyup)="tree.treeModel.filterNodes(filter.value)" placeholder="filter nodes"/> -->
    <input #filter2 (keyup)="filterFn(filter2.value, tree.treeModel)" placeholder="filter"/>
    <button (click)="tree.treeModel.clearFilter()">Clear Filter</button>

    <tree-root #tree [focused]="true" [nodes]="nodes" (onActiveChanged)="OnEvent($event)" >
      <ng-template #treeNodeTemplate let-node let-index="index">
        <span (click)="go(tree.treeModel)">{{ node.data.name }}</span>
      </ng-template>
    </tree-root>    
    </div>
 `,
  styles: [''],
  
  
})
export class treeFilterComponent {
  
  @Input() nodes : any;

  filterFn(value, treeModel: TreeModel) {
    treeModel.filterNodes((node) => fuzzysearch(value, node.data.name));
  }

  go(treeModel : TreeModel) {

    var focusedArray = <Array<ITreeNode>> treeModel.activeNodes
    if (focusedArray.length > 0)
    {

      var focused : ITreeNode = focusedArray[0];
      var data = <INode>focused.data;

      debugger;
    
    }
    
  }
  
}

function fuzzysearch (needle, haystack) {
  const haystackLC = haystack.toLowerCase();
  const needleLC = needle.toLowerCase();

  const hlen = haystack.length;
  const nlen = needleLC.length;

  if (nlen > hlen) {
    return false;
  }
  if (nlen === hlen) {
    return needleLC === haystackLC;
  }
  outer: for (let i = 0, j = 0; i < nlen; i++) {
    const nch = needleLC.charCodeAt(i);

    while (j < hlen) {
      if (haystackLC.charCodeAt(j++) === nch) {
        continue outer;
      }
    }
    return false;
  }
  return true;
}
