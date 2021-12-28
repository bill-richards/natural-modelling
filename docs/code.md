# Getting the source

Because some of the solutions in this repository have dependencies on shared libraries, those libraries (which can be) are included as submodules. 
When you get the source using

```cmd
git clone https://github.com/bill-richards/natural-modelling.git
```

be sure to also call

```cmd
git submodule update --init --recursive
```

## Updating the source

It is important to note that a _**submodule**_ is linked to a repository at a specific version

If the submodules do not refelect the current state of the super repositories -because commits have been made to those super repositories- then you will most likely want to update the submodules to incorporate those changes.

### Update all submodules

When you need to update all of the submodules (to be point to the current HEAD of their repositories), run

```cmd
git submodule update --remote --merge
```

### Update individual submodules

When you want to update an individual submodule (and not ALL submodules) run the following

```cmd
git submodule update <path-to-submodule>
```

so, for example, to update **only** the _gsdc-common_ submodule within the _evolution_ folder

```cmd
git submodule update evolution/libraries/gsdc-common
```

### After updating the submodule(s)

Don't forget that you will also have to **push** those updates back to the remote repository

```cmd
git push
```

# Making changes to a submodule

This could be made a whole lot easier, I'm sure, but we have what we have and so, you will need to follow the instructions carefully.

If we work on a feature, it is possible or even probable that we will need to also extend the functionality expressed within a submodule project. Do your work and check it in!

## No matter how many submodules contain change

```cmd
git submodule foreach git add . # iterates over all contained submodules and adds changes
```

### For each submdule containing local changes

```cmd
cd <submodule-name>
git commit -a -m "sub module change description"
cd ..
git add .
git commit -a -m "Commiting submodule changes from superproject"
git push --recurse-submodules=on-demand
```

### Another way to get all up to date submodules

```cmd
git submodule foreach git pull origin development
```

#### _Nota bene_

Not yet tested, but soon to be ... it is believed that when using the Visual Studio Git extension, and changes are made to a submodule, all changes are propagated back to their respective supr repositories.
