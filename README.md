# ARcardapp README

## Contribution guidelines

Clone the repository.
`git clone git@github.com:TortoiseLeaf/ARcardapp.git`

`main` is locked and only used for shipping releases to Unity Cloud. If working on a new ticket or feature, branch from `dev`.

Name it after the Jira ticket you are working on, e.g. `ARC - 12: add a 3D prefab`

Before remerging remember to first pull `dev` into your branch:


`git checkout dev && git pull`

`git checkout <your-branch>`

`git merge dev`


then push those changes to your branch. 

You now have the latest version of `dev` on your branch and are ready to merge! 

Merge your branch into `dev` and someone will review it in a pull request.

**important:** remember to delete the branch when it's no longer needed.



## End-User Training materials

install the latest version from `Link TBD`



Open the application on your phone and scan this target image:


![target-img](https://github.com/TortoiseLeaf/ARcardapp/blob/docs-imgs/docs-imgs/trgt-img.png?raw=true)


This should produce the AR UI like so:


![ARcard-Demo](https://github.com/TortoiseLeaf/ARcardapp/blob/docs-imgs/docs-imgs/ARcard-demo.gif?raw=true)


This is a map of the UI functions:


![ARcard-figma](https://github.com/TortoiseLeaf/ARcardapp/blob/docs-imgs/docs-imgs/ARcard-figma.png?raw=true)


The buttons along the bottom of the card should correspond with different sections of the card-issuers profile data. 

From left to right: 
- To hear the Professionals' bio, press the first button.
- For interests press the second
- For Education history press the third.


We hope you enjoy the application.
