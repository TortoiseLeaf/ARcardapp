ARcardapp README

### Contribution

To contribute, first clone the repository.
`git@github.com:TortoiseLeaf/ARcardapp.git`

the `main` branch is locked and will only be used for releases. So we will be working primarily from the `dev` branch.

create a branch from `dev` and name it after the Jira ticket you are working on, e.g. `ARC - 12: add a 3D prefab`

When you have completed the ticket, remember to pull dev into your branch:

`git checkout dev && git pull`

`git checkout <your-branch>`

`git merge dev`


then push those changes to your branch. 

You now have the latest version of `dev` on your branch and are ready to merge! 

Merge your branch into `dev` and someone will review it in a pull request.