# Getting the source

[<< back to start](index.md)

Because some of the solutions in this repository have dependencies on shared libraries, those libraries (which can be) are included as submodules, and so there is an extra step involved in cloning the repository.

```cmd
# Clone the superproject
git clone https://github.com/bill-richards/natural-modelling.git

# ... and retrieve the submodules
git submodule update --init --recursive
```

## Updating the source

It is important to note that a _**submodule**_ is linked to a repository at a specific version

If the submodules do not refelect the current state of their repositories -because commits have been made to those repositories- then you will most likely want to update the submodules to incorporate those changes.

```cmd
# Updates all of the submodules 
git submodule update --remote --merge

# Updates an individual submodule 
git submodule update <path-to-submodule>
```

so, for example, to update **only** the _gsdc-common_ submodule within the _evolution_ superproject

```cmd
git submodule update evolution/libraries/gsdc-common
```

**N.B.** After updating the submodule(s) don't forget that you will also have to **push** those updates back to the remote repository using `git push`

## Making changes to a submodule

This could be made a whole lot easier, I'm sure, but we have what we have and so, you will need to follow the instructions carefully.

If we work on a feature, it is possible or even probable that we will need to also extend the functionality expressed within a submodule project. Do your work and check it in!

### No matter how many submodules contain change

You can add all of your changes to submodules either from within the submodule's root (i.e. it's local folder), which you would then need to do **_for each submodule with changes_**; alternatively, you can add the changes for all of those submodules **_from the root of the superproject_**, as follows.

```cmd
# iterates over all contained submodules and adds changes
git submodule foreach git add . 
```

### For each submodule containing local changes

```cmd
# move to the submodule's directory
cd <submodule-name>

# commit all changes for this submodule
git commit -a -m "sub module change description"

# push the changes to HEAD of the remote _develop_ branch
git push origin HEAD:develop

# return to the project root
cd ..

# adding everything here includes adding the committed submodule
git add .

# commit the superproject
git commit -a -m "Committing submodule changes from superproject"

# push all changes to the remote repository
git push --recurse-submodules=on-demand
```

### Another way to update  all submodules

```cmd
git submodule foreach git pull origin develop
```

## Adding a new submodule

When a new project is to be added, generally it needs to go in the correct space. For example, if the project is concerned with NLP, then it should be made a submodule of the [**_natural-language-processing_**](https://github.com/bill-richards/natural-language-processing) repository, this way it will also be included within the [**_natural-modelling_**](https://github.com/bill-richards/natural-modelling) repository.

```cmd
# go to superproject
cd <super-project-root>

# create submodule
git submodule add <new-project-url> <destination-folder>

# commit the change
git commit -a -m "Added <new-project> submodule"

# push to remote origin
git push 
```

For example, if we created a new project called _dialect-model_ which comes under the banner of NLP (natural language processing), we would performt he following

```cmd
cd natural-language-processing
git submodule add https://github.com/bill-richards/dialect-model dialect
git commit -a -m "Added new dialect submodule"
git push
```
